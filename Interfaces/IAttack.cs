using RPG_Character_System.Characters;
using RPG_Character_System.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Interfaces
{
    public interface IAttack
    {
        void DealDamage(Character target, Weapon weapon, TakeDamageService takeDamageS);
    }
}
