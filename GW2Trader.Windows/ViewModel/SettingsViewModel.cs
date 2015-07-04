using GW2Trader.Desktop.Command;
using GW2Trader.Desktop.Data;

namespace GW2Trader.Desktop.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IDbBuilder _dbBuilder;

        public SettingsViewModel(IDbBuilder dbBuilder)
        {
            ViewModelName = "Settings";
            _dbBuilder = dbBuilder;
        }

        public void UpdateDatabase()
        {
            _dbBuilder.UpdateDatabase();
        }

        #region Commands

        private RelayCommand _updateDatabaseCommand;

        public RelayCommand UpdateDatabaseCommand
        {
            get
            {
                if (_updateDatabaseCommand == null)
                    _updateDatabaseCommand = new UpdateDatabaseCommand();
                return _updateDatabaseCommand;
            }
        }

        #endregion
    }
}
