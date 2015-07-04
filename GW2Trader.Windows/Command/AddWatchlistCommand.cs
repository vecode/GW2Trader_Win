using System;
using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
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
