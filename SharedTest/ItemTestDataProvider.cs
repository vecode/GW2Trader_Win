using GW2TPApiWrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTest
{
    public class ItemTestDataProvider
    {
        /// data taken from https://api.guildwars2.com/v2/items?ids=20323,19700,30689
        public List<Item> GetTestItems()
        {
            return new List<Item>
            {
                new Item
                {
                    Id = 20323,
                    Name = "Unidentified Dye", 
                    Description = "Double-click to identify one random dye color,..",
                    Details = new Details {Type = "GiftBox"},
                    IconUrl = @"https://render.guildwars2.com/file/109A6B04C4E577D9266EEDA21CC30E6B800DD452/66587.png",
                    Level = 0,
                    Rarity = "Basic",
                    VendorValue = 0,
                    Type = "Container"
                },
                new Item
                {
                    Id = 19700,
                    Name = "Mithril Ore", 
                    Description = "Refine into Ingots.",
                    IconUrl = @"https://render.guildwars2.com/file/E90FE803CDC205CDEB13FE03694D4D04757ACF5D/65928.png",
                    Level = 0,
                    Rarity = "Basic",
                    VendorValue = 7,
                    Type = "CraftingMaterial"
                },
                new Item
                {
                    Id = 30689,
                    Name = "Eternity", 
                    Details = new Details {Type = "Weapon"},
                    IconUrl = @"https://render.guildwars2.com/file/A30DA1A1EF05BD080C95AE2EF0067BADCDD0D89D/456014.png",
                    Level = 80,
                    Rarity = "Legendary",
                    VendorValue = 100000,
                    Type = "Greatsword"
                },
            };
        }
    }
}
