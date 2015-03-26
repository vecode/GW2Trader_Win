using GW2Trader.Model;
using GW2Trader.MVVM;
using GW2Trader.ViewModel;

namespace GW2Trader.Command
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