using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2TPApiWrapper.Entities;


namespace GW2TPApiWrapper.Wrapper
{
    public interface ITradingPostApiWrapper
    {
        /// <summary>
        /// </summary>
        /// <returns>Returns an array of ids of all avaible items on the trading post.</returns>
        int[] ItemIds();

        /// <summary>
        /// </summary>
        /// <param name="id">Id of the item</param>
        /// <returns>Returns an instance of Item</returns>
        Item ItemDetails(int id);

        /// <summary>
        /// </summary>
        /// <param name="ids">Ids of the items</param>
        /// <returns>Returns a list of ItemDetails</returns>
        List<Item> ItemDetails(int[] ids);

        /// <summary>
        /// </summary>
        /// <param name="id">Id of the item to get the listings for.</param>
        /// <returns>
        /// Returns an instance of ItemListing containing all buy orders and sell offers.2
        /// </returns>
        ItemListing Listings(int id);

        /// <summary>
        /// </summary>
        /// <param name="id">Ids of the items to get the listings for.</param>
        /// <returns>
        /// Returns a list of ItemListing containing all buy orders and sell offers.
        /// </returns>
        List<ItemListing> Listings(int[] ids);

        ItemPrice Price(int id);

        //List<ItemPrice> Price(int ids);
    }
}
