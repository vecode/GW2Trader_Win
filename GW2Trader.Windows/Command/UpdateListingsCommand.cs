using GW2Trader.Desktop.Model;
using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
{
    public class UpdateListingsCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            object[] parameters = parameter as object[];
            GameItemModel itemToUpdate = parameters[0] as GameItemModel;
            var viewModel = (ItemSearchViewModel)parameters[1];
            viewModel.UpdateListings(itemToUpdate);
        }
    }
}
