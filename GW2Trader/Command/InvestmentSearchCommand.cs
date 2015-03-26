using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Extension;
using GW2Trader.ViewModel;

namespace GW2Trader.Command
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
