using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.ViewModel;

namespace GW2Trader_Windows.Command
{
    public class UpdateAllItemsCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            MainViewModel viewModel = parameter as MainViewModel;
            if (viewModel != null) viewModel.UpdateCommerceDataOfAllItems();
        }
    }
}
