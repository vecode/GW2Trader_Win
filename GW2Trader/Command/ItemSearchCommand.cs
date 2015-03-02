using GW2Trader.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Data;
using GW2Trader.Model;
using GW2Trader.MVVM;


namespace GW2Trader.Command
{
    public class ItemSearchCommand
    {
        public class SearchCommand : RelayCommand
        {
            public override bool CanExecute(object parameter)
            {
                return true;
            }

            public override void Execute(object parameter)
            {
                ItemSearchViewModel viewModel = parameter as ItemSearchViewModel;
                PaginatedObservableCollection<GameItemModel> itemCollection = (parameter as ItemSearchViewModel).Items;

                itemCollection.Filter = item =>
                {
                    GameItemModel itemModel = item as GameItemModel;

                    if (itemModel == null) return false;

                    if (itemModel.RestrictionLevel < viewModel.MinLvl)
                        return false;

                    if (itemModel.RestrictionLevel > viewModel.MaxLvl)
                        return false;

                    //if (itemModel.Margin < viewModel.MinMargin)
                    //    return false;

                    //if (itemModel.Margin > viewModel.MaxMargin && viewModel.MaxMargin != 0)
                    //    return false;

                    //if (itemModel.ROI < viewModel.MinROI)
                    //    return false;

                    //if (itemModel.ROI > viewModel.MaxROI && viewModel.MaxROI != 0)
                    //    return false;

                    if (!(string.IsNullOrWhiteSpace(viewModel.Keyword) || string.IsNullOrEmpty(viewModel.Keyword)))
                    {
                        if (!itemModel.Name.ToLower().Contains(viewModel.Keyword.ToLower()))
                            return false;
                    }
                    return true;
                };
                viewModel.UpdateCommerData();
            }
        }

        public class SearchResetCommand : RelayCommand
        {
            public override bool CanExecute(object parameter)
            {
                return true;
            }

            public override void Execute(object parameter)
            {
                ItemSearchViewModel viewModel = parameter as ItemSearchViewModel;
                viewModel.Keyword = string.Empty;
                viewModel.MinLvl = 0;
                viewModel.MaxLvl = 80;
                viewModel.MinMargin = 0;
                viewModel.MaxMargin = 0;
                viewModel.MinROI = 0;
                viewModel.MaxROI = 0;

                viewModel.SearchCommand.Execute(viewModel);
            }
        }

        public class MoveToNextPageCommand : RelayCommand
        {
            public override bool CanExecute(object parameter)
            {
                return (parameter as ItemSearchViewModel).Items.CanMoveToNextPage();
            }

            public override void Execute(object parameter)
            {
                (parameter as ItemSearchViewModel).Items.MoveToNextPage();
            }
        }

        public class MoveToPreviousPageCommand : RelayCommand
        {
            public override bool CanExecute(object parameter)
            {
                return (parameter as ItemSearchViewModel).Items.CanMoveToPreviousPage();
            }

            public override void Execute(object parameter)
            {
                (parameter as ItemSearchViewModel).Items.MoveToPreviousPage();
            }
        }
    }
}
