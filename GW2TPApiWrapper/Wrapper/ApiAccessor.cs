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
        private const string ItemsApiUrl = @"https://api.guildwars2.com/v2/items/";
        private const string ListingsApiUrl = @"https://api.guildwars2.com/v2/commerce/listings/";
        private const string PricesApiUrl = @"https://api.guildwars2.com/v2/commerce/prices/";

        public ApiAccessor()
        {
            // prevent auto detecting of proxy settings to improve performance
            WebRequest.DefaultWebProxy = null;
        }

        public Stream ItemIds()
        {
            return new WebClient().OpenRead(ListingsApiUrl);
        }

        public Stream ItemDetails(int id)
        {
            return new WebClient().OpenRead(ApiUrlFormatter.FormatUrl(ItemsApiUrl, id));
        }

        public Stream ItemDetails(int[] ids)
        {
            // TODO setting headers needed?
            WebClient webClient = new WebClient();
            webClient.Headers["Content-Type"] = "application/json;charset=UTF-8";
            webClient.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");
            //webClient.Headers.Add("Accept-Language", " en-US");
            return webClient.OpenRead(ApiUrlFormatter.FormatUrl(ItemsApiUrl, ids));
        }

        public Stream Listings(int id)
        {
            return new WebClient().OpenRead(ApiUrlFormatter.FormatUrl(ListingsApiUrl, id));
        }

        public Stream Listings(int[] ids)
        {
            return new WebClient().OpenRead(ApiUrlFormatter.FormatUrl(ListingsApiUrl, ids));
        }

        public Stream Prices(int[] ids)
        {
            WebClient webClient = new WebClient();
            return webClient.OpenRead(ApiUrlFormatter.FormatUrl(PricesApiUrl, ids));
        }
    }
}
