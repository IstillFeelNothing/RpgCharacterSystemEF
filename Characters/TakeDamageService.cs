using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Characters
{
    public class TakeDamageService
    {
        public void TakeDamage(Character character, int damage)
        {
            character.Health -= damage;

            if (character.Health < 0)
            {
                character.Health = 0;
                character.RaiseCharacterDied();
            }
        }
    }
}
