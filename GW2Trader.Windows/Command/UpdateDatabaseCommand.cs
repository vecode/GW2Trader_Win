using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
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
            viewModel.UpdateDatabase();
        }
    }
}
