using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.ViewModel;

namespace GW2Trader_Windows.Command
{
    public class AddInvestmentListCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            InvestmentViewModel viewModel = parameter as InvestmentViewModel;
            if (viewModel.InvestmentListName == null)
            {
                return false;
            }
            string trimmedName = viewModel.InvestmentListName.Trim();
            return !String.IsNullOrWhiteSpace(trimmedName);
        }

        public override void Execute(object parameter)
        {
            InvestmentViewModel viewModel = parameter as InvestmentViewModel;
            viewModel.AddInvestmentList();
        }
    }
}
