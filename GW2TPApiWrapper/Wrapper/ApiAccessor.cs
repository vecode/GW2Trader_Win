using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using GW2TPApiWrapper.Util;

namespace GW2TPApiWrapper.Wrapper
{
    public class ApiAccessor : IApiAccessor
    {
        private readonly string _itemsApiUrl = @"https://api.guildwars2.com/v2/items/";
        private readonly string _listingsApiUrl = @"https://api.guildwars2.com/v2/commerce/listings/";
        private readonly string _pricesApiUrl = @"https://api.guildwars2.com/v2/commerce/prices/";

        public ApiAccessor()
        {
            // prevent auto detecting proxy settings to improve performance
            WebRequest.DefaultWebProxy = null;
        }

        public Stream ItemIds()
        {
            return new WebClient().OpenRead(_listingsApiUrl);
        }

        public Stream ItemDetails(int id)
        {
            return new WebClient().OpenRead(ApiUrlFormatter.FormatUrl(_itemsApiUrl, id));
        }

        public Stream ItemDetails(int[] ids)
        {
            // TODO setting headers needed?
            WebClient webClient = new WebClient();
            webClient.Headers["Content-Type"] = "application/json;charset=UTF-8";
            webClient.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");
            //webClient.Headers.Add("Accept-Language", " en-US");
            return webClient.OpenRead(ApiUrlFormatter.FormatUrl(_itemsApiUrl, ids));
        }

        public Stream Listings(int id)
        {
            return new WebClient().OpenRead(ApiUrlFormatter.FormatUrl(_listingsApiUrl, id));
        }

        public Stream Listings(int[] ids)
        {
            return new WebClient().OpenRead(ApiUrlFormatter.FormatUrl(_listingsApiUrl, ids));
        }

        public Stream Prices(int[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
