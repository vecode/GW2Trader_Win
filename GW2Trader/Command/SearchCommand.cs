using GW2Trader.Model;
using GW2Trader.MVVM;
using GW2Trader.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Command
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
}
