using GW2TPApiWrapper.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader_Windows.Util
{
    public class WinWebClient : IWebClient
    {
        private WebClient _webClient = new WebClient();

        public System.IO.Stream OpenRead(string url)
        {
            return _webClient.OpenRead(url);
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}
