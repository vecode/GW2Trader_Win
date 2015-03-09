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
            ItemSearchViewModel vm = parameter as ItemSearchViewModel;
            Console.WriteLine("pagecount: " + vm.Items.PageCount);
            Console.WriteLine("currentpage: " + vm.Items.CurrentPage);
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
