using RPG_Character_System.Characters;
using RPG_Character_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Systems
{
    public class MagicAttackService
    {
        public void ExecuteMagicAttack(Character attacker, Character target, TakeDamageService damageService)
        {
            if (attacker is IMagic magicUser)
            {
                magicUser.CastSpell(target, damageService);
            }
        }
    }
}
