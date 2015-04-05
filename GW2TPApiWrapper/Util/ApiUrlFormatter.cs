using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Util
{
    public static class ApiUrlFormatter
    {
        private static readonly char _seperator;
        static ApiUrlFormatter()
        {
            _seperator = ',';
        }
        public static string FormatUrl(string apiUrl, int id)
        {
            return apiUrl + '/' + id;
        }
        public static string FormatUrl(string apiurl, int[] ids)
        {
            string formattedUrl = new String(apiurl.ToArray());
            formattedUrl += "?ids=";
            foreach (int id in ids)
            {
                formattedUrl += id;
                if (id != ids.Last())
                    formattedUrl += _seperator;
            }
            return formattedUrl;
        }
    }
}