using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.ViewModel;

namespace GW2Trader.Command
{
    public class DeleteWatchlistCommand : RelayCommand
    {

        public override bool CanExecute(object parameter)
        {
            return ((parameter as WatchlistViewModel).SelectedWatchlist != null);
        }

        public override void Execute(object parameter)
        {
            WatchlistViewModel viewModel = parameter as WatchlistViewModel;
            viewModel.RemoveWatchlist(viewModel.SelectedWatchlist);
        }
    }
}
