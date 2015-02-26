using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2TPApiWrapper.Enum;

namespace GW2TPApiWrapper.Entities
{
    /// <summary>
    /// Represents the result of an api call to https://api.guildwars2.com/v2/items/id
    /// </summary>
    /// api documentation is avaible at http://wiki.guildwars2.com/wiki/API:2/items
 
    public class Item
    {
        // [JsonProperty("item_id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ItemType.Type Type { get; set; }

        public int Level { get; set; }

        public ItemRarity.Rarity Rarity { get; set; }

        [JsonProperty("vendor_value")]
        public int VendorValue { get; set; }

        [JsonProperty("icon")]
        public String IconUrl { get; set; }
    }
}
