using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Command;
using GW2Trader.Data;

namespace GW2Trader.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IDbBuilder _dbBuilder;

        public SettingsViewModel(IDbBuilder dbBuilder)
        {
            ViewModelName = "Settings";
            _dbBuilder = dbBuilder;
        }

        public void UpdataDatabase()
        {
            _dbBuilder.UpdateDatabase();   
        }


        #region Commands

        private RelayCommand _updataDatabaseCommand;

        public RelayCommand UpdateDatabaseCommand
        {
            get
            {
                if (_updataDatabaseCommand == null)
                    _updataDatabaseCommand = new UpdateDatabaseCommand();
                return _updataDatabaseCommand;
            }
        }

        #endregion
    }
}
