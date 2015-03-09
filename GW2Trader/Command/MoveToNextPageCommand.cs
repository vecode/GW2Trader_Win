using GW2Trader.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Command
{
    public class MoveToNextPageCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {            
            return (parameter as ItemSearchViewModel).Items.CanMoveToNextPage();
        }

        public override void Execute(object parameter)
        {
            ItemSearchViewModel viewModel = parameter as ItemSearchViewModel;
            viewModel.Items.MoveToNextPage();
            viewModel.UpdateCommerceData();
        }
    }
}
