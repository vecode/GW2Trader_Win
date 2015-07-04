 using System.Collections.Generic;
using GW2Trader.Model;

namespace GW2Trader.Manager
{
    public interface IItemManager
    {
        List<Item> Search(
            string keyword,
            string rarity = null,
            string type = null,
            string subType = null,
            int minLevel = 0,
            int maxLevel = 100,
            int minMargin = 0,
            int maxMargin = 0,
            int minRoi = 0,
            int maxRoi = 0,
            int pageSize = 10,
            int page = 0 );

        void UpdatePrices(Item item);

        void UpdatePrices(List<Item> items);

        void UpdatePriceListings(Item item);

        void UpdatePriceListings(List<Item> items);

        void BuildItemDb();

        Item GetItem(int id);
    }
}
