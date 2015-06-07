using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Wrapper
{
    public interface IApiAccessor
    {
        /// <summary>
        /// Makes an api call and returns the json result stream containing the ids 
        /// of all items on the trading post.
        /// </summary>
        /// <returns>Returns a stream containing an array of ids of all avaible items on the trading post formatted as json</returns>
        Stream ItemIds();

        /// <summary>
        /// Makes an api call and returns the json result stream containing the details
        /// of an item.
        /// </summary>
        /// <param name="id">Id of the requested item</param>
        /// <returns>Returns a stream containing details of an item formatted as json</returns>
        Stream ItemDetails(int id);
        Stream ItemDetails(int[] ids);

        /// <summary>
        /// Makes an api call and returns the json result stream containing buy and sell
        /// listings of an item.
        /// </summary>
        /// <param name="id">Id of the requested item</param>
        /// <returns>Returns a stream containing an items buy and sell listings and their quantites formatted as json.</returns>
        Stream Listings(int id);
        Stream Listings(int[] ids);

        Stream Prices(int[] ids);
      }
}
