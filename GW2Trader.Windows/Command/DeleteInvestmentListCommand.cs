using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
{
    public class DeleteInvestmentListCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            InvestmentViewModel viewModel = parameter as InvestmentViewModel;
            return viewModel.SelectedWatchlist != null;
        }

        public override void Execute(object parameter)
        {
            InvestmentViewModel viewModel = parameter as InvestmentViewModel;
            viewModel.DeleteInvestmentList(viewModel.SelectedWatchlist);
        }
    }
}
