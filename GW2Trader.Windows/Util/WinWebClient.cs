using System.Net;
using GW2Trader.ApiWrapper.Util;

namespace GW2Trader.Desktop.Util
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
