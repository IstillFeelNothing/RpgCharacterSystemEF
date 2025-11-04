using RPG_Character_System.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Interfaces
{
    public interface IBattleStart
    {
        public void StartBattle(Character c1, Character c2);
    }
}
