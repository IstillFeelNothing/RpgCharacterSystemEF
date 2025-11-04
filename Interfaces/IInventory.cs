using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Interfaces
{
    public interface IInventory<T>
    {
        void AddItem(T item);
        void RemoveItem(T item);
        IEnumerable<T> GetItems();
    }
}
