using GW2Trader.Desktop.View;
using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
{
    public class AddInvestmentCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            NewInvestmentViewModel viewModel = (NewInvestmentViewModel)(parameter as NewInvestmentWindow).DataContext;

            if (viewModel.SelectedItem == null)
            {
                return false;
            }

            if (viewModel.BuyPrice.Value == 0)
            {
                return false;
            }

            if (viewModel.Quantity == 0)
            {
                return false;
            }
            return true;
        }

        public override void Execute(object parameter)
        {
            NewInvestmentWindow window = parameter as NewInvestmentWindow;
            NewInvestmentViewModel viewModel = (NewInvestmentViewModel)window.DataContext;

            viewModel.FinalizeResult();
            window.Close();
        }
    }
}
