using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using GW2Trader.MVVM;

namespace GW2Trader.Model
{
    public abstract class WatchlistModel<T> : ObservableObject
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        private string _name;

        [Required]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value; 
                RaisePropertyChanged("Name");
            }
        }

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