using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.Model;
using GW2Trader_Windows.ViewModel;

namespace GW2Trader_Windows.Command
{
    public class AddWatchlistCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            WatchlistViewModel viewModel = parameter as WatchlistViewModel;
            if (viewModel.WatchlistName == null)
            {
                return false;
            }
            string trimmedName = viewModel.WatchlistName.Trim();
            return !String.IsNullOrWhiteSpace(trimmedName);
        }

        public override void Execute(object parameter)
        {
            WatchlistViewModel viewModel = parameter as WatchlistViewModel;
            viewModel.AddWatchlist();
        }
    }
}
