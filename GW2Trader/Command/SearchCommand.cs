using System.Linq;
using GW2Trader.Extension;
using GW2Trader.ViewModel;

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
            var viewModel = parameter as ItemSearchViewModel;
            var itemCollection = (parameter as ItemSearchViewModel).Items;

            itemCollection.Filter = i =>
            {
                var item = i;

                if (item == null) return false;

                if (item.RestrictionLevel < viewModel.MinLvl)
                    return false;

                if (item.RestrictionLevel > viewModel.MaxLvl)
                    return false;

                if (item.Margin < viewModel.MinMargin && viewModel.MinMargin != 0)
                    return false;

                if (item.Margin > viewModel.MaxMargin && viewModel.MaxMargin != 0)
                    return false;

                if (viewModel.SelectedRarity.Name != "All" && viewModel.SelectedRarity.Name != item.Rarity)
                    return false;


                if (item.ROI <= viewModel.MinROI && viewModel.MinROI != 0)
                    return false;

                if (item.ROI > viewModel.MaxROI && viewModel.MaxROI != 0)
                    return false;

                if (viewModel.SelectedType != "All" && viewModel.SelectedType != null)
                {
                    if (viewModel.SelectedType != item.Type)
                    {
                        return false;
                    }
                    if (viewModel.SelectedSubType != "All" && viewModel.SelectedSubType != item.SubType)
                    {
                        return false;
                    }
                }

                if (!viewModel.Keyword.IsNullOrEmptyOrWhiteSpace())
                {
                    if (!item.Name.ToLower().Contains(viewModel.Keyword.ToLower()))
                        return false;
                }
                return true;
            };
        }
    }
}