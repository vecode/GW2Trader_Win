using GW2Trader_Windows.ViewModel;

namespace GW2Trader_Windows.Command
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
