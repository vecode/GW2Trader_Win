using GW2Trader.Desktop.View;
using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
{
    public class OpenNewInvestmentDialogCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            InvestmentViewModel viewModel = parameter as InvestmentViewModel;
            return viewModel.SelectedWatchlist != null;
        }

        public override void Execute(object parameter)
        {
            InvestmentViewModel viewModel = parameter as InvestmentViewModel;

            NewInvestmentViewModel newInvestmentViewModel = new NewInvestmentViewModel(viewModel.SharedItems);

            NewInvestmentWindow newInvestmentWindow = new NewInvestmentWindow
            {
                DataContext = newInvestmentViewModel
            };

            newInvestmentWindow.ShowDialog();

            if (newInvestmentViewModel.Investment != null)
            {
                viewModel.AddInvestment(newInvestmentViewModel.Investment);
            }
        }
    }
}
