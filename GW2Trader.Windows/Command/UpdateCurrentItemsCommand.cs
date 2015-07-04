using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
{
    public class UpdateCurrentItemsCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            MainViewModel viewModel = parameter as MainViewModel;
            return viewModel != null && viewModel.ChildViews[viewModel.SelectedTabIndex] is IItemViewer;
        }

        public override void Execute(object parameter)
        {
            MainViewModel viewModel = parameter as MainViewModel;
            viewModel.UpdateCommerceDataOfShownItems();
        }
    }
}
