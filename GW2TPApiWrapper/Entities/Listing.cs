using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Entities
{
    public class Listing
    {
        public int Quantity{ get; set; }

        [JsonProperty("unit_price")]
        public int UnitPrice { get; set; }
    }
}
