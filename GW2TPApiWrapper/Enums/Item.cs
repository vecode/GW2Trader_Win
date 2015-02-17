using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Enums
{
    public class Item
    {
        public enum ItemRarity
        {
            Ascended,
            Basic,
            Exotic,
            Fine,
            Junk,
            Legendary,
            Masterwork,
            Rare
        }

        public enum ItemType
        {
            Armor,
            Back,
            Bag,
            Consumable,
            Container,
            CraftingMaterial,
            Gathering,
            Gizmo,
            MiniPet,
            Tool,
            Trait,
            Trinket,
            Trophy,
            UpgradeComponent,
            Weapon
        }
    }
}
