using RPG_Character_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Inventory
{
    public class Inventory<T> : IInventory<T>
    {
        private List<T> _items = new List<T>();

        public void AddItem(T item) 
        { 
            _items.Add(item);
        }
        public void RemoveItem(T item) 
        { 
            _items.Remove(item);
        }
        public IEnumerable<T> GetItems()
        {
            return _items;
        }
    }
}
