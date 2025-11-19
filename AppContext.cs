using Microsoft.EntityFrameworkCore;
using RPG_Character_System.Characters;
using RPG_Character_System.Items;
using RPG_Character_System.Systems;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System
{
    public class AppContext : DbContext
    {
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Mage> Mages { get; set; }
        public DbSet<Warrior> Warriors { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<CharacterDto> CharacterDTO { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("RpgDatabase");

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().ToTable("Characters");
            modelBuilder.Entity<Warrior>().ToTable("Warriors");
            modelBuilder.Entity<Mage>().ToTable("Mages");

            modelBuilder.Entity<Item>().ToTable("Items");
            modelBuilder.Entity<Weapon>().ToTable("Weapons");

            modelBuilder.Entity<CharacterDto>().HasNoKey();

            modelBuilder.Entity<Character>()
               .HasOne(c => c.EquippedWeapon)
               .WithOne(w => w.Character)
               .HasForeignKey<Character>(c => c.EquippedWeaponId)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Character>()
               .HasMany(c => c.Quests)
               .WithMany(q => q.Characters)
               .UsingEntity(j => j.ToTable("CharacterQuests"));

            modelBuilder.Entity<Weapon>().HasData(
                new Weapon { Id = 1, Name = "Steel Sword", AttackPower = 20 },
                new Weapon { Id = 2, Name = "Arcane Staff", AttackPower = 25 }
            );

            modelBuilder.Entity<Warrior>().HasData(
                new Warrior
                {
                    Id = 1,
                    Name = "Thorin",
                    Health = 120,
                    Level = 5,
                    Experience = 300,
                    BonusDamage = 110,
                    EquippedWeaponId = 1
                }
            );

            modelBuilder.Entity<Mage>().HasData(
                new Mage
                {
                    Id = 2,
                    Name = "Elora",
                    Health = 80,
                    Level = 6,
                    Experience = 400,
                    SpellDamage = 100,
                    EquippedWeaponId = 2
                }
            );

            modelBuilder.Entity<Quest>().HasData(
                new Quest
                {
                    Id = 1,
                    Title = "Defeat the Dragon",
                    Description = "Slay the dragon in the northern mountains."
                },
                new Quest
                {
                    Id = 2,
                    Title = "Find the Lost Sword",
                    Description = "Recover the legendary blade from ancient ruins."
                }
            );
        }
    }
}
