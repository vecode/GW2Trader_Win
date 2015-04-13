using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;

namespace GW2Trader.Service
{
    public interface ICommerceService
    {
        /// <summary>
        /// Updates the buy and sell price of the specified items.
        /// </summary>
        void UpdatePrices(IEnumerable<GameItemModel> items);

        /// <summary>
        /// Updates the buy and sell listing of the specified item.
        /// </summary>
        void UpdateListings(GameItemModel item);

        ///// <summary>
        ///// Updates the buy and sell listing of the specified items.
        ///// </summary>
        //void UpdateListings(IEnumerable<GameItemModel> item);

        ///// <summary>
        ///// Updates the price history of the specified item
        ///// </summary>
        //void UpdatePriceHistory(GameItemModel item);
    }
}
