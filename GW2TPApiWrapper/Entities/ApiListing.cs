using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace GW2Trader.ApiWrapper.Entities
{
    public class ApiListing : GW2TPApiResponse
    {
        public int Quantity{ get; set; }

        [JsonProperty("unit_price")]
        public int UnitPrice { get; set; }
    }
}
