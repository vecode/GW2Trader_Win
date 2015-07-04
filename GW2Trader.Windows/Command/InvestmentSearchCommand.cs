using System.Linq;
using GW2Trader.Desktop.Extension;
using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
{
    public class KeywordSearchCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            NewInvestmentViewModel viewModel = parameter as NewInvestmentViewModel;
            return viewModel.Keyword != null;
        }

        public override void Execute(object parameter)
        {
            NewInvestmentViewModel viewModel = parameter as NewInvestmentViewModel;
            var itemCollection = (parameter as NewInvestmentViewModel).Items;

            itemCollection.Filter = i =>
            {
                var item = i;

                if (!viewModel.Keyword.IsNullOrEmptyOrWhiteSpace())
                {
                    if (!item.Name.ToLower().Contains(viewModel.Keyword.ToLower()))
                        return false;
                }
                return true;
            };
            viewModel.SelectedItem = viewModel.Items.Any() ? viewModel.Items[0] : null;
        }
    }
}
