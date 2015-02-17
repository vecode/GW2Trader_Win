using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Entities
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
        /// <param name="id">Id of the item to get the price for.</param>
        /// <returns>
        /// Returns an instance of ItemPrice containing the highest buy order, 
        /// lowest sell offer and their quantities
        /// </returns>
        ItemPrice ItemPrice(int id);


        /// <summary>
        /// </summary>
        /// <param name="id">Id of the item to get the listings for.</param>
        /// <returns>
        /// Returns an instance of ItemListing containing all buy orders and sell offers.2
        /// </returns>
        ItemListing Listings(int id);
    }
}
