using GW2Trader.Desktop.Model;
using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
{
    public class AddItemsToWatchlistCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            object[] parameters = parameter as object[];
            ItemSearchViewModel viewModel = parameters[0] as ItemSearchViewModel;
            viewModel.AddItemsToWatchlist(parameters[1] as ItemWatchlistModel);
        }
    }
}