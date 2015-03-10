using System.Collections.Generic;
using GW2Trader.Model;

namespace GW2Trader.Data
{
    public interface IApiDataUpdater
    {
        void UpdateItemData(GameItemModel item);
        void UpdateCommerceData(GameItemModel item);
        void UpdateCommerceData(IList<GameItemModel> items);
    }
}