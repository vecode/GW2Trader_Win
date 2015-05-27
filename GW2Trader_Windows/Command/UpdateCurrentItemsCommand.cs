using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.ViewModel;

namespace GW2Trader_Windows.Command
{
    public class UpdateCurrentItemsCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            MainViewModel viewModel = parameter as MainViewModel;
            return viewModel != null && viewModel.ChildViews[viewModel.SelectedTabIndex] is IItemViewer;
        }

        public override void Execute(object parameter)
        {
            MainViewModel viewModel = parameter as MainViewModel;
            viewModel.UpdateCommerceDataOfShownItems();
        }
    }
}
