using RPG_Character_System.Interfaces;
using RPG_Character_System.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Characters
{
    public class Mage : Character, IAttack, IMagic
    {
        public int SpellDamage { get; set; }

        public Mage()
        {
            ClassType = CharacterClassType.Mage;
        }

        public void DealDamage(Character target, Weapon weapon, TakeDamageService takeDamageS) 
        {
            takeDamageS.TakeDamage(target, weapon.AttackPower);
        }
        void IMagic.CastSpell(Character target, TakeDamageService takeDamageS) 
        {
            takeDamageS.TakeDamage(target, SpellDamage);
        }
    }
}
