using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace GW2TPApiWrapper.Entities
{
    public class ApiAccessor : IApiAccessor
    {
        private readonly String _itemsApiUrl = @"https://api.guildwars2.com/v2/items/";
        private readonly String _listingsApiUrl = @"https://api.guildwars2.com/v2/commerce/listings/";
        private readonly String _pricesApiUrl = @"https://api.guildwars2.com/v2/commerce/prices/";
        private readonly String _seperator = ",";

        public string ItemIds()
        {
            return ApiRequest(_itemsApiUrl);
        }

        public string ItemDetails(int id)
        {
            String apiUrl = FormatApiUrl(_itemsApiUrl, id);
            return ApiRequest(apiUrl);
        }

        public string ItemPrice(int id)
        {
            String apiUrl = FormatApiUrl(_pricesApiUrl, id);
            return ApiRequest(apiUrl);
        }

        public string Listings(int id)
        {
            String apiUrl = FormatApiUrl(_listingsApiUrl, id);
            return ApiRequest(apiUrl);
        }

        /// <summary>
        /// Appends a id to apiUrl.
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="id"></param>
        /// <returns>Returns the apiUrl appended with id.</returns>
        private string FormatApiUrl(String apiUrl, int id)
        {
            return apiUrl + id.ToString();
        }

        /// <summary>
        /// Calls the api and returns the result.
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <returns>Returns result of api call.</returns>
        private string ApiRequest(string apiUrl)
        {
            string jsonResult = String.Empty;
            using (var webClient = new WebClient())
            {
                try
                {
                    jsonResult = webClient.DownloadString(apiUrl);
                }
                catch (Exception ex) { return String.Empty; }               
            }
            if (IsIdNotFoundResponse(jsonResult))
                return null;
            return jsonResult;
        }

        public bool IsIdNotFoundResponse(string jsonResponse)
        {
            JsonSchema schema = JsonSchema.Parse(@"
                {
                  'type': 'object',
                  'properties': {
                    'text': {
                      'id': 'text',
                      'type': 'string',
                      'enum': [ null, 'no such id' ]
                    }
                  },
                  'additionalProperties': false,
                  'required': true
                }");
            JObject response;
            try
            {
                response = JObject.Parse(jsonResponse);
            }
            catch (Exception ex)
            {
                return false;
            }
            return response.IsValid(schema);
        }
    }
}
