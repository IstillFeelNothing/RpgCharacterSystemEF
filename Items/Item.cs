using RPG_Character_System.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Items
{
    public abstract class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
