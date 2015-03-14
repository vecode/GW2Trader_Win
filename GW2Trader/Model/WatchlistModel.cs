using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GW2Trader.Model
{
    public abstract class WatchlistModel<T>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        public virtual ObservableCollection<T> Items { get; set; }

        public WatchlistModel()
        {
            Items = new ObservableCollection<T>();
        } 

        public WatchlistModel(string name)
        {
            Name = name;
            Items = new ObservableCollection<T>();
        }
    }
}