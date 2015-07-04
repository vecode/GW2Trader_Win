using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using GW2Trader.ApiWrapper.Util;

namespace GW2Trader_Android.Util
{
    public class AndroidWebClient : IWebClient
    {
        private WebClient _client = new WebClient();

        public System.IO.Stream OpenRead(string url)
        {
            return _client.OpenRead(url);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
