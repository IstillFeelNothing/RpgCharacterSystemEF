using RPG_Character_System.Inventory;
using RPG_Character_System.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Characters
{
    public class InventoryService
    {
        public void UseItem(Character character, Inventory<Item> inventory, Item item)
        {
            if(item is Potion potion)
            {
                character.Health += potion.HealAmount;
            }

            inventory.RemoveItem(item);
            character.RaiseItemUsed();
        }
    }
}
