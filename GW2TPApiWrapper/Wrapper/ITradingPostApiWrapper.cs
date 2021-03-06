﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.ApiWrapper.Entities;

namespace GW2Trader.ApiWrapper.Wrapper
{
    public interface ITradingPostApiWrapper
    {
        /// <summary>
        /// </summary>
        /// <returns>Returns an array of ids of all avaible items on the trading post.</returns>
        IEnumerable<int> ItemIds();

        /// <summary>
        /// </summary>
        /// <param name="id">Id of the item</param>
        /// <returns>Returns an instance of Item</returns>
        ApiItem ItemDetails(int id);

        /// <summary>
        /// </summary>
        /// <param name="ids">Ids of the items</param>
        /// <returns>Returns a list of ItemDetails</returns>
        IEnumerable<ApiItem> ItemDetails(IEnumerable<int> ids);

        /// <summary>
        /// </summary>
        /// <param name="id">Id of the item to get the listings for.</param>
        /// <returns>
        /// Returns an instance of ItemListing containing all buy orders and sell offers.2
        /// </returns>
        ApiItemListing Listings(int id);

        /// <summary>
        /// </summary>
        /// <param name="ids">Ids of the items to get the listings for.</param>
        /// <returns>
        /// Returns a list of ItemListing containing all buy orders and sell offers.
        /// </returns>
        IEnumerable<ApiItemListing> Listings(IEnumerable<int> ids);

        IEnumerable<ApiItemPrice> Prices(IEnumerable<int> ids);
    }
}
