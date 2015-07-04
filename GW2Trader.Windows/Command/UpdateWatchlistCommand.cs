using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
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
