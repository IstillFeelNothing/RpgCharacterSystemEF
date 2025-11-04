using RPG_Character_System.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System
{
    public delegate void DamageHandler(Character attacker, Character target, int damage);
    public delegate void LevelUpHandler(Character character);
}
