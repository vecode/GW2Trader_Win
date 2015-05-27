using GW2Trader_Windows.Model;
using GW2Trader_Windows.MVVM;
using GW2Trader_Windows.ViewModel;

namespace GW2Trader_Windows.Command
{
    public class MoveToPreviousPageCommand : RelayCommand
    {
        public override bool CanExecute(object parameter)
        {
            return (parameter as PaginatedObservableCollection<GameItemModel>).CanMoveToPreviousPage;
        }

        public override void Execute(object parameter)
        {
            var paginatedCollection = parameter as PaginatedObservableCollection<GameItemModel>;
            paginatedCollection.MoveToPreviousPage();
        }
    }
}