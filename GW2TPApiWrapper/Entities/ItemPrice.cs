using GW2TPApiWrapper.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Entities
{
    /// <summary>
    /// Represents the result of an api call to https://api.guildwars2.com/v2/commerce/prices/id .
    /// </summary>
    /// api documentation is avaible at http://wiki.guildwars2.com/wiki/API:2/commerce/prices

    public class ItemPrice
    {
        public int Id { get; set; }
        public Listing Buys { get; set; }
        public Listing Sells { get; set; }
    }
}
