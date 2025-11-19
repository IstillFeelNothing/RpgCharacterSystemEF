using RPG_Character_System.Characters;
using RPG_Character_System.Inventory;
using RPG_Character_System.Items;
using RPG_Character_System.Systems;

namespace RPG_Character_System
{
    public class Program
    {
        static void Main(string[] args)
        {
            //LinqMethods.JoinExample();
            //LinqMethods.GroupByExample();
            //LinqMethods.DistinctExample();
            //LinqMethods.SetOperationsExample();

            //LinqMethods.EagerLoadingExample();
            //LinqMethods.ExplicitLoadingExample();
            //LinqMethods.LazyLoadingExample();

            //LinqMethods.NoTrackingExample();
            //LinqMethods.StoredProcedureExample();
            //LinqMethods.SqlFunctionExample();
        }

        static void CreateData()
        {
            using (var context = new AppContext())
            {
                var sword = new Weapon { Name = "Wood Sword", AttackPower = 20 };
                var staff = new Weapon { Name = "Arcane Staff", AttackPower = 25 };

                var warrior = new Warrior
                {
                    Name = "Bilbo",
                    Health = 120,
                    Level = 5,
                    Experience = 300,
                    EquippedWeapon = sword
                };

                var mage = new Mage
                {
                    Name = "Fiona",
                    Health = 80,
                    Level = 6,
                    Experience = 400,
                    EquippedWeapon = staff
                };

                var quest = new Quest
                {
                    Title = "Defeat the Turtle",
                    Description = "Slay the Turtle in the northern mountains.",
                    Characters = new List<Character> { warrior, mage }
                };

                context.AddRange(sword, staff, warrior, mage, quest);
                context.SaveChanges();
            }
        }

        static void ReadCharacters()
        {
            using (var context = new AppContext())
            {
                Console.WriteLine("\nСписок персонажів");

                var characters = context.Characters
                    .Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.Level,
                        Weapon = c.EquippedWeapon != null ? c.EquippedWeapon.Name : "None"
                    })
                    .ToList();

                foreach (var c in characters)
                    Console.WriteLine($"[{c.Id}] {c.Name} (Lv. {c.Level}) — Weapon: {c.Weapon}");
            }
        }
        static void UpdateCharacter(string characterName)
        {
            using (var context = new AppContext())
            {
                var character = context.Characters.FirstOrDefault(c => c.Name == characterName);
                if (character != null)
                {
                    character.Level += 1;
                    character.Experience += 200;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"\nПерсонаж '{characterName}' не знайдений.");
                }
            }
        }
        static void DeleteWeapon(string weaponName)
        {
            using (var context = new AppContext())
            {
                var weapon = context.Weapons.FirstOrDefault(w => w.Name == weaponName);
                if (weapon != null)
                {
                    context.Weapons.Remove(weapon);
                    context.SaveChanges();

                    Console.WriteLine($"\nЗброю '{weapon.Name}' видалено.");
                }
                else
                {
                    Console.WriteLine($"\nЗброю '{weaponName}' не знайдено.");
                }
            }
        }

        static void ReadWeapons()
        {
            using (var context = new AppContext())
            {
                Console.WriteLine("\nСписок зброї");

                var weapons = context.Weapons.ToList();
                if (weapons.Count == 0)
                {
                    Console.WriteLine("(Порожньо)");
                    return;
                }

                foreach (var w in weapons)
                    Console.WriteLine($"{w.Name} (Attack {w.AttackPower})");
            }
        }
    }
}