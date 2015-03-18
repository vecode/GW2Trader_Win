using GW2Trader.ViewModel;

namespace GW2Trader.Command
{
    public class MoveToPreviousPageCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return (parameter as ItemSearchViewModel).Items.CanMoveToPreviousPage();
        }

        public override void Execute(object parameter)
        {
            var viewModel = parameter as ItemSearchViewModel;
            viewModel.Items.MoveToPreviousPage();
        }
    }
}