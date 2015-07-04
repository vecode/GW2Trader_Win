using GW2Trader.Desktop.Model;
using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
{
    public class DeleteWatchlistItemCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            object[] parameters = parameter as object[];
            WatchlistViewModel viewModel = parameters[0] as WatchlistViewModel;
            GameItemModel item = parameters[1] as GameItemModel;
            viewModel.DeleteWatchlistItem(item);
        }
    }
}
