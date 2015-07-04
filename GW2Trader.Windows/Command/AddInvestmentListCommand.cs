using System;
using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
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
