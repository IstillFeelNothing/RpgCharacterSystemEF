using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPG_Character_System.Characters;
using RPG_Character_System.Inventory;
using RPG_Character_System.Items;
using RPG_Character_System.Systems;

namespace RPG_Character_System.Utilities
{
    public static class ConcurrencyGenerators
    {
        private static int characterCounter = 0;
        private static int weaponCounter = 0;
        private static readonly object counterLock = new object();
        private static readonly Mutex consoleMutex = new Mutex();
        
        private static readonly SemaphoreSlim dbSemaphore = new SemaphoreSlim(3);

        public static void GenerateWithThreads(int threadsCount, int itemsPerThread)
        {
            var threads = new List<Thread>();

            for (int t = 0; t < threadsCount; t++)
            {
                var thread = new Thread(() =>
                {
                    for (int i = 0; i < itemsPerThread; i++)
                    {
                        int idx;
                        Monitor.Enter(counterLock);
                        try
                        {
                            idx = ++characterCounter;
                        }
                        finally
                        {
                            Monitor.Exit(counterLock);
                        }

                        var charName = $"NPC_Thread_{Thread.CurrentThread.ManagedThreadId}_{idx}";

                        var weapon = new Weapon { Name = $"Weapon_{idx}", AttackPower = 10 + (idx % 20) };
                        var warrior = new Warrior
                        {
                            Name = charName,
                            Health = 100 + (idx % 50),
                            Level = (idx % 10) + 1,
                            Experience = idx * 10,
                            EquippedWeapon = weapon
                        };

                        Task.Run(async () =>
                        {
                            await dbSemaphore.WaitAsync();
                            try
                            {
                                using var ctx = new AppContext();
                                await ctx.Weapons.AddAsync(weapon);
                                await ctx.Characters.AddAsync(warrior);
                                await ctx.SaveChangesAsync();
                            }
                            finally
                            {
                                dbSemaphore.Release();
                            }
                        }).GetAwaiter().GetResult();

                        consoleMutex.WaitOne();
                        try
                        {
                            Console.WriteLine($"[Thread] Created: {warrior.Name} with {weapon.Name}");
                        }
                        finally
                        {
                            consoleMutex.ReleaseMutex();
                        }
                    }
                })
                { IsBackground = true };

                threads.Add(thread);
                thread.Start();
            }

            foreach (var th in threads)
                th.Join();
        }

        public static async Task GenerateWithTasksAsync(int tasksCount, int itemsPerTask, CancellationToken cancellation = default)
        {
            var taskList = new List<Task>();

            for (int t = 0; t < tasksCount; t++)
            {
                var task = Task.Run(async () =>
                {
                    for (int i = 0; i < itemsPerTask; i++)
                    {
                        cancellation.ThrowIfCancellationRequested();

                        int idx;
                        Monitor.Enter(counterLock);
                        try
                        {
                            idx = ++weaponCounter;
                        }
                        finally
                        {
                            Monitor.Exit(counterLock);
                        }

                        var weapon = new Weapon { Name = $"TPL_Weapon_{idx}", AttackPower = 5 + (idx % 30) };
                        var mage = new Mage
                        {
                            Name = $"Mage_TPL_{Task.CurrentId ?? t}_{idx}",
                            Health = 60 + (idx % 40),
                            Level = (idx % 12) + 1,
                            Experience = idx * 15,
                            EquippedWeapon = weapon
                        };

                        await dbSemaphore.WaitAsync(cancellation);
                        try
                        {
                            using var ctx = new AppContext();
                            await ctx.Weapons.AddAsync(weapon, cancellation);
                            await ctx.Characters.AddAsync(mage, cancellation);
                            await ctx.SaveChangesAsync(cancellation);
                        }
                        finally
                        {
                            dbSemaphore.Release();
                        }

                        consoleMutex.WaitOne();
                        try
                        {
                            Console.WriteLine($"[Task] Created: {mage.Name} with {weapon.Name}");
                        }
                        finally
                        {
                            consoleMutex.ReleaseMutex();
                        }
                    }
                }, cancellation);

                taskList.Add(task);
            }

            await Task.WhenAll(taskList);
        }

        public static async Task ConcurrentReadAsync(int parallelReaders, int pageSize = 50)
        {
            using (var ctx = new AppContext())
            {
                var total = await ctx.Characters.CountAsync();
                if (total == 0)
                {
                    Console.WriteLine("No characters to read.");
                    return;
                }
            }

            var tasks = new List<Task>();

            int pages = (int)Math.Ceiling((double)await GetTotalCharactersAsync() / pageSize);

            for (int p = 0; p < pages; p++)
            {
                int pageIndex = p;
                var task = Task.Run(async () =>
                {
                    using var ctx = new AppContext();
                    var items = await ctx.Characters
                                         .OrderBy(c => c.Id)
                                         .Skip(pageIndex * pageSize)
                                         .Take(pageSize)
                                         .Include(c => c.EquippedWeapon)
                                         .ToListAsync();

                    consoleMutex.WaitOne();
                    try
                    {
                        Console.WriteLine($"[Reader] Page {pageIndex + 1} - Loaded {items.Count} items:");
                        foreach (var c in items)
                        {
                            Console.WriteLine($"  - [{c.Id}] {c.Name} Lv{c.Level} Weapon: {(c.EquippedWeapon != null ? c.EquippedWeapon.Name : "None")}");
                        }
                    }
                    finally
                    {
                        consoleMutex.ReleaseMutex();
                    }
                });

                tasks.Add(task);

                if (tasks.Count >= parallelReaders)
                {
                    await Task.WhenAll(tasks);
                    tasks.Clear();
                }
            }

            if (tasks.Count > 0)
                await Task.WhenAll(tasks);
        }

        private static async Task<int> GetTotalCharactersAsync()
        {
            using var ctx = new AppContext();
            return await ctx.Characters.CountAsync();
        }
    }
}
