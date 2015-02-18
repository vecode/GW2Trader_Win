using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Model
{
    public class Watchlist <T>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        public String Description { get; set; }
        public List<T> Items { get; set; }
    }
}
