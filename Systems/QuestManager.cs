using RPG_Character_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Systems
{
    public class QuestManager : IQuestManager
    {
        public event EventHandler<Quest> OnQuestCompleted;

        private List<Quest> _quests = new List<Quest>();

        public void AddQuest(Quest quest) 
        { 
            if(!_quests.Contains(quest))
            {
                _quests.Add(quest);
            }
        }
        public void CompleteQuest(Quest quest) 
        {
            if(_quests.Contains(quest) && !quest.IsCompleted)
            {
                quest.IsCompleted = true;
                RaiseQuestCompleted(quest);
            }
        }

        private void RaiseQuestCompleted(Quest quest)
        {
            OnQuestCompleted?.Invoke(this, quest);
        }
    }
}
