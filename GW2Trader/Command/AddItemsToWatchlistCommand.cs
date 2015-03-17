using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;
using GW2Trader.ViewModel;

namespace GW2Trader.Command
{
    public class AddItemsToWatchlistCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            object[] parameters = parameter as object[];
            ItemSearchViewModel viewModel = parameters[0] as ItemSearchViewModel;
            viewModel.AddItemsToWatchlist(parameters[1] as ItemWatchlistModel);
        }
    }
}