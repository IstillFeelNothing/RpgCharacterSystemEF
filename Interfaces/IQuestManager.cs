using RPG_Character_System.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Interfaces
{
    internal interface IQuestManager
    {
        public void AddQuest(Quest quest);
        public void CompleteQuest(Quest quest);

    }
}
