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
        private const string ItemsApiUrl = @"https://api.guildwars2.com/v2/items";
        private const string ListingsApiUrl = @"https://api.guildwars2.com/v2/commerce/listings";
        private const string PricesApiUrl = @"https://api.guildwars2.com/v2/commerce/prices";

        public ApiAccessor()
        {
            // prevent auto detecting of proxy settings to improve performance
            WebRequest.DefaultWebProxy = null;
        }

        public Stream ItemIds()
        {
            return new WebClient().OpenRead(ListingsApiUrl);
        }

        public bool TryGetItemIds(out Stream val)
        {
            try
            {
                val = ItemIds();
                return true;
            }
            catch (System.Exception ex)
            {
                val = null;
                return false;
            }
        }

        public Stream ItemDetails(int id)
        {
            return new WebClient().OpenRead(ApiUrlFormatter.FormatUrl(ItemsApiUrl, id));
        }

        public bool TryGetItemDetails(int id, out Stream val)
        {
            try
            {
                val = ItemDetails(id);
                return true;
            }
            catch (System.Exception ex)
            {
                val = null;
                return false;
            }
        }

        public Stream ItemDetails(int[] ids)
        {
            WebClient webClient = new WebClient();
            return webClient.OpenRead(ApiUrlFormatter.FormatUrl(ItemsApiUrl, ids));
        }

        public bool TryGetItemDetails(int[] ids, out Stream val)
        {
            try
            {
                val = ItemDetails(ids);
                return true;
            }
            catch (System.Exception ex)
            {
                val = null;
                return false;
            }
        }

        public Stream Listings(int id)
        {
            return new WebClient().OpenRead(ApiUrlFormatter.FormatUrl(ListingsApiUrl, id));
        }

        public bool TryGetListings(int id, out Stream val)
        {
            try
            {
                val = Listings(id);
                return true;
            }
            catch (System.Exception ex)
            {
                val = null;
                return false;
            }
        }

        public Stream Listings(int[] ids)
        {
            return new WebClient().OpenRead(ApiUrlFormatter.FormatUrl(ListingsApiUrl, ids));
        }

        public bool TryGetListings(int[] ids, out Stream val)
        {
            try
            {
                val = Listings(ids);
                return true;
            }
            catch (System.Exception ex)
            {
                val = null;
                return false;
            }
        }

        public Stream Prices(int[] ids)
        {
            WebClient webClient = new WebClient();
            return webClient.OpenRead(ApiUrlFormatter.FormatUrl(PricesApiUrl, ids));
        }

        public bool TryGetPrices(int[] ids, out Stream val)
        {
            try
            {
                val = Prices(ids);
                return true;
            }
            catch (System.Exception ex)
            {
                val = null;
                return false;
            }
        }

    }
}
