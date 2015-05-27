using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.ViewModel;

namespace GW2Trader_Windows.Command
{
    public class UpdateWatchlistCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            WatchlistViewModel viewModel = parameter as WatchlistViewModel;
            return viewModel.SelectedWatchlist != null;
        }

        public override void Execute(object parameter)
        {
            WatchlistViewModel viewModel = parameter as WatchlistViewModel;
            viewModel.UpdateWatchlist();
        }
    }
}
