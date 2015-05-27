using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.Model;
using GW2Trader_Windows.View;
using GW2Trader_Windows.ViewModel;

namespace GW2Trader_Windows.Command
{
    public class DeleteWatchlistItemCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            object[] parameters = parameter as object[];
            WatchlistViewModel viewModel = parameters[0] as WatchlistViewModel;
            GameItemModel item = parameters[1] as GameItemModel;
            viewModel.DeleteWatchlistItem(item);
        }
    }
}
