using GW2Trader.Desktop.ViewModel;

namespace GW2Trader.Desktop.Command
{
    public class SearchResetCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var viewModel = parameter as ItemSearchViewModel;
            viewModel.Keyword = string.Empty;
            viewModel.MinLvl = 0;
            viewModel.MaxLvl = 80;
            viewModel.MinMargin = 0;
            viewModel.MaxMargin = 0;
            viewModel.MinROI = 0;
            viewModel.MaxROI = 0;

            viewModel.Items.Clear();
            viewModel.Items.Filter = null;
        }
    }
}