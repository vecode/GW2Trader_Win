using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Entities
{
    public class ItemListing
    {
        public int Id { get; set; }
        public Listing[] Buys { get; set; }
        public Listing[] Sells { get; set; }
    }
}
