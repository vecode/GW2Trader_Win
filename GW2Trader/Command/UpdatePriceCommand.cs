using GW2Trader.ViewModel;

namespace GW2Trader.Command
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