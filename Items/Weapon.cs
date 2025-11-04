using RPG_Character_System.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Items
{
    public class Weapon : Item
    {
        public int AttackPower { get; set; }
        public int? CharacterId { get; set; }
        public Characters.Character? Character { get; set; }
    }
}
