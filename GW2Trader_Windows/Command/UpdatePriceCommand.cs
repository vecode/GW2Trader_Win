using GW2Trader_Windows.ViewModel;

namespace GW2Trader_Windows.Command
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