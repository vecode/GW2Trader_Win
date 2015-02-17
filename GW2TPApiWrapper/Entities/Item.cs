using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Entities
{
    /// <summary>
    /// Represents the result of an api call to https://api.guildwars2.com/v2/items/id
    /// </summary>
    /// api documentation is avaible at http://wiki.guildwars2.com/wiki/API:2/items
 
    public class Item
    {
        [JsonProperty("item_id")]
        public int ID { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public Enums.Item.ItemType Type { get; set; }

        public int Level { get; set; }

        public Enums.Item.ItemRarity Rarity { get; set; }

        [JsonProperty("vendor_value")]
        public int VendorValue { get; set; }

        [JsonProperty("icon_file_id")]
        public int IconFileID { get; set; }

        [JsonProperty("icon_file_signature")]
        public String IconFileSignature { get; set; }
    }
}
