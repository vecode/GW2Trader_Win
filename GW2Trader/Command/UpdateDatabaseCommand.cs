using GW2Trader.ViewModel;

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
            viewModel.UpdateDatabase();
        }
    }
}
