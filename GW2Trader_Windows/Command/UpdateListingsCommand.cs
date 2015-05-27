using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.Model;
using GW2Trader_Windows.ViewModel;

namespace GW2Trader_Windows.Command
{
    public class UpdateListingsCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            object[] parameters = parameter as object[];
            GameItemModel itemToUpdate = parameters[0] as GameItemModel;
            var viewModel = (ItemSearchViewModel)parameters[1];
            viewModel.UpdateListings(itemToUpdate);
        }
    }
}
