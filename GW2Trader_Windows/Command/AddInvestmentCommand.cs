using GW2Trader_Windows.View;
using GW2Trader_Windows.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader_Windows.Command
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
