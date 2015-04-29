using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.ViewModel;
using  System.Diagnostics.Contracts;

namespace GW2Trader.Command
{
    public class UpdateDatabaseCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            SettingsViewModel viewModel = parameter as SettingsViewModel;
            viewModel.UpdataDatabase();
        }
    }
}
