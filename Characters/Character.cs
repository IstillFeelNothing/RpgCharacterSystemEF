using RPG_Character_System.Inventory;
using RPG_Character_System.Items;
using RPG_Character_System.Systems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Characters
{
    public abstract class Character
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int Health { get; set; }

        [Range(0, 100)]
        public int Level { get; set; }
        public int? EquippedWeaponId { get; set; }
        public virtual Weapon? EquippedWeapon {  get; set; }
        public int Experience { get; set; } = 1;
        public CharacterClassType ClassType { get; protected set; }
        public virtual List<Quest> Quests { get; set; }

        public event EventHandler OnCharacterDied;
        public event LevelUpHandler OnLevelUp;
        public event EventHandler OnItemUsed;

        public void RaiseCharacterDied() 
        { 
            OnCharacterDied?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseLevelUp() 
        {
            OnLevelUp?.Invoke(this);
        }
        public void RaiseItemUsed() 
        {
            OnItemUsed?.Invoke(this, EventArgs.Empty);
        }
    }
}
