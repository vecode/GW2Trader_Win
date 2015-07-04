using System.Collections.Generic;
using GW2Trader.Desktop.Model;

namespace GW2Trader.Desktop.Data
{
    public interface IApiDataUpdater
    {
        void UpdateItemData(GameItemModel item);

        void UpdatePrices(GameItemModel item);
        void UpdatePrices(IList<GameItemModel> items);
        void UpdatePricesParallel(IList<GameItemModel> items);            

        void UpdateListings(GameItemModel item);
        void UpdateListings(IList<GameItemModel> items);
        void UpdateListingsParallel(GameItemModel items);
        void UpdateListingsParallel(IList<GameItemModel> items);

        void UpdateCommerceDataParallel(IList<GameItemModel> items);
    }
}