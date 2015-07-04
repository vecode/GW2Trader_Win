using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
{
    public class UpdatePriceCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var viewModel = parameter as ItemSearchViewModel;
            viewModel.UpdateCommerceData();
        }
    }
}