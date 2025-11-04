using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Characters
{
    public class ExperienceService
    {
        public void GainEperience(Character character, int exp)
        {
            character.Experience += exp;

            if (character.Experience >= 1000)
            {
                character.Level++;
                character.Experience -= 1000;
                character.RaiseLevelUp();
            }
        }
    }
}
