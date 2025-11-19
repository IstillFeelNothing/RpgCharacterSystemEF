using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System
{
    public static class LinqMethods
    {
        public static void JoinExample()
        {
            using (var db = new AppContext())
            {

                var query = from c in db.Characters
                            join w in db.Weapons on c.EquippedWeaponId equals w.Id
                            select new { c.Name, Weapon = w.Name };

                Console.WriteLine("JOIN:");
                foreach (var x in query)
                    Console.WriteLine($"{x.Name} має зброю: {x.Weapon}");
            }
        }

        public static void GroupByExample()
        {
            using (var db = new AppContext())
            {

                var groups = db.Characters
                .GroupBy(c => c.Level)
                .Select(g => new { Level = g.Key, Count = g.Count() })
                .ToList();

                Console.WriteLine("\nGROUP BY:");
                foreach (var g in groups)
                    Console.WriteLine($"Level {g.Level}: {g.Count} персонажів");
            }
        }

        public static void DistinctExample()
        {
            using (var db = new AppContext())
            {

                var levels = db.Characters
                .Select(c => c.Level)
                .Distinct()
                .ToList();

                Console.WriteLine("\nDISTINCT:");
                foreach (var l in levels)
                    Console.WriteLine($"Level: {l}");
            }
        }

        public static void SetOperationsExample()
        {
            using (var db = new AppContext())
            {

                var mages = db.Mages.Select(m => m.Name);
                var warriors = db.Warriors.Select(w => w.Name);

                Console.WriteLine("\nUNION:");
                foreach (var n in mages.Union(warriors))
                    Console.WriteLine(n);

                Console.WriteLine("\nINTERSECT:");
                foreach (var n in mages.Intersect(warriors))
                    Console.WriteLine(n);

                Console.WriteLine("\nEXCEPT (Mages - Warriors):");
                foreach (var n in mages.Except(warriors))
                    Console.WriteLine(n);
            }
        }

        public static void EagerLoadingExample()
        {
            using (var db = new AppContext())
            {

                var characters = db.Characters
                .Include(c => c.EquippedWeapon)
                .Include(c => c.Quests)
                .ToList();

                Console.WriteLine("\nEAGER LOADING:");
                foreach (var c in characters)
                {
                    Console.WriteLine($"{c.Name}, weapon: {c?.EquippedWeapon?.Name}");
                }
            }
        }

        public static void ExplicitLoadingExample()
        {
            using (var db = new AppContext())
            {

                var character = db.Characters.First();

                db.Entry(character).Reference(c => c.EquippedWeapon).Load();
                db.Entry(character).Collection(c => c.Quests).Load();

                Console.WriteLine("\nEXPLICIT LOADING:");
                Console.WriteLine($"{character.Name}, weapon: {character?.EquippedWeapon?.Name}");
            }
        }

        public static void LazyLoadingExample()
        {
            using (var db = new AppContext())
            {

                var c = db.Characters.First();

                Console.WriteLine("\nLAZY LOADING:");
                Console.WriteLine($"{c.Name} → {c.EquippedWeapon?.Name}");
            }
        }

        public static void NoTrackingExample()
        {
            using (var db = new AppContext())
            {

                var charNoTracking = db.Characters.AsNoTracking().First();

                Console.WriteLine("\nAS NO TRACKING:");
                Console.WriteLine($"Character: {charNoTracking.Name}");

                charNoTracking.Level += 1;

                using var update = new AppContext();
                update.Characters.Update(charNoTracking);
                update.SaveChanges();
            }
        }

        public static void StoredProcedureExample()
        {
            using (var db = new AppContext())
            {

                var result = db.CharacterDTO
                .FromSqlRaw("EXEC GetCharactersAboveLevel @level={0}", 5)
                .ToList();

                Console.WriteLine("\nStored Procedure:");
                foreach (var c in result)
                    Console.WriteLine(c.Name);
            }
        }

        public static void SqlFunctionExample()
        {
            using (var db = new AppContext())
            {

                var value = db.CharacterDTO
                .FromSqlRaw("SELECT dbo.GetAverageLevel() AS Level")
                .Select(r => (double)r.Level)
                .First();

                Console.WriteLine("\nSQL Function:");
                Console.WriteLine("Average Level = " + value);
            }
        }
    }
}
