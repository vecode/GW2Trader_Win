using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Model
{
    public class Watchlist <T>
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public List<T> Items { get; set; }
    }
}
