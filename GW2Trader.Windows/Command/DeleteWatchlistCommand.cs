using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
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
