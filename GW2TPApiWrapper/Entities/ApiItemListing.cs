using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Entities
{
    public class ApiItemListing : GW2TPApiResponse
    {
        public int Id { get; set; }
        public ApiListing[] Buys { get; set; }
        public ApiListing[] Sells { get; set; }
    }
}
