using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GW2Trader.Model
{
    public abstract class WatchlistModel<T>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        public String Description { get; set; }
        public virtual ICollection<T> Items { get; set; }
    }
}