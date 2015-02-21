using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Wrapper
{
    public interface IApiAccessor
    {
        /// <summary>
        /// Makes an api call and returns the json result string containing the ids 
        /// of all items on the trading post.
        /// </summary>
        /// <returns>Returns an array of ids of all avaible items on the trading post formatted as json</returns>
        String ItemIds();

        /// <summary>
        /// Makes an api call and returns the json result string containing the details
        /// of an item.
        /// </summary>
        /// <param name="id">Id of the requested item</param>
        /// <returns>Returns details of an item formatted as json</returns>
        String ItemDetails(int id);

        /// <summary>
        /// Makes an api call and returns the json result string containing the prices
        /// of an item.
        /// </summary>
        /// <param name="id">Id of the requested item</param>
        /// <returns>
        /// Returns an items highest buy order, lowest sell offer and their quantites
        /// formatted as json.
        /// </returns>
        String ItemPrice(int id);

        /// <summary>
        /// Makes an api call and returns the json result string containing buy and sell
        /// listings of an item.
        /// </summary>
        /// <param name="id">Id of the requested item</param>
        /// <returns>Returns an items buy and sell listings and their quantites formatted as json.</returns>
        String Listings(int id);
    }
}
