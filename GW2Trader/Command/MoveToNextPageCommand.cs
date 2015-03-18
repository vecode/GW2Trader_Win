using GW2Trader.ViewModel;

namespace GW2Trader.Command
{
    public class MoveToNextPageCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return (parameter as ItemSearchViewModel).Items.CanMoveToNextPage();
        }

        public override void Execute(object parameter)
        {
            var viewModel = parameter as ItemSearchViewModel;
            viewModel.Items.MoveToNextPage();
        }
    }
}