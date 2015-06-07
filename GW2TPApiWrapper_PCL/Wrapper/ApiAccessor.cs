using System.IO;
using System.Net;
using GW2TPApiWrapper.Util;

namespace GW2TPApiWrapper.Wrapper
{
    public class ApiAccessor : IApiAccessor
    {
        private const string ItemsApiUrl = @"https://api.guildwars2.com/v2/items";
        private const string ListingsApiUrl = @"https://api.guildwars2.com/v2/commerce/listings";
        private const string PricesApiUrl = @"https://api.guildwars2.com/v2/commerce/prices";

        private IWebClientProvider _webClientProvider;

        public ApiAccessor(IWebClientProvider webClientProvider)
        {
            // prevent auto detecting of proxy settings to improve performance
            WebRequest.DefaultWebProxy = null;
            _webClientProvider = webClientProvider;
        }

        public Stream ItemIds()
        {
            using (IWebClient webClient = _webClientProvider.GetWebClient())
            {
                return webClient.OpenRead(ListingsApiUrl);
            }            
        }

        public Stream ItemDetails(int id)
        {
            using (IWebClient webClient = _webClientProvider.GetWebClient())
            {
                return webClient.OpenRead(ApiUrlFormatter.FormatUrl(ItemsApiUrl, id));
            }
        }

        public Stream ItemDetails(int[] ids)
        {
            using (IWebClient webClient = _webClientProvider.GetWebClient())
            {
                try
                {
                    return webClient.OpenRead(ApiUrlFormatter.FormatUrl(ItemsApiUrl, ids));
                }
                catch (WebException)
                {
                    return null;
                }
            }
        }

        public Stream Listings(int id)
        {
            using (IWebClient webClient = _webClientProvider.GetWebClient())
            {
                return webClient.OpenRead(ApiUrlFormatter.FormatUrl(ListingsApiUrl, id));
            }
        }

        public Stream Listings(int[] ids)
        {
            using (IWebClient webClient = _webClientProvider.GetWebClient())
            {
                return webClient.OpenRead(ApiUrlFormatter.FormatUrl(ListingsApiUrl, ids));
            }
        }

        public Stream Prices(int[] ids)
        {
            using (IWebClient webClient = _webClientProvider.GetWebClient())
            {
                return webClient.OpenRead(ApiUrlFormatter.FormatUrl(PricesApiUrl, ids));
            }
        }
    }
}
