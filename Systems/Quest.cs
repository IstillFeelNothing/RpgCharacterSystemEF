using RPG_Character_System.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Systems
{
    public class Quest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; internal set; }
        public virtual List<Character> Characters { get; set; }

        public Quest() { }
        public Quest(string title, string description)
        {
            Title = title;
            Description = description;
            IsCompleted = false;
        }
    }
}
