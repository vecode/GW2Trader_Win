using GW2Trader.Desktop.Model;
using GW2Trader.Desktop.MVVM;

namespace GW2Trader.Desktop.Command
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