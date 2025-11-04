using RPG_Character_System.Interfaces;
using RPG_Character_System.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Characters
{
    public class Warrior : Character, IAttack
    {
        public int BonusDamage {  get; set; }

        public Warrior()
        {
            ClassType = CharacterClassType.Warrior;
        }

        public void DealDamage(Character target, Weapon weapon, TakeDamageService takeDamageS) 
        {
            takeDamageS.TakeDamage(target, weapon.AttackPower+BonusDamage);
        }
    }
}
