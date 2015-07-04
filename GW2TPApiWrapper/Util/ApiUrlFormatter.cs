using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.ApiWrapper.Util
{
    public static class ApiUrlFormatter
    {
        private const char Seperator = ',';

        public static string FormatUrl(string apiUrl, int id)
        {
            return apiUrl + '/' + id;
        }
        public static string FormatUrl(string apiurl, int[] ids)
        {
            StringBuilder formattedUrl = new StringBuilder(apiurl);
            formattedUrl.Append("?ids=");

            string idsToAppend = string.Join(Seperator.ToString(), ids.Select(x => x.ToString()).ToArray());
            formattedUrl.Append(idsToAppend);

            return formattedUrl.ToString();
        }
    }
}