using System.IO;
using System.Net;
using GW2Trader.ApiWrapper.Util;

namespace GW2Trader.Android.Util
{
    public class AndroidWebClient : IWebClient
    {
        private readonly WebClient _client = new WebClient();

        public Stream OpenRead(string url)
        {
            return _client.OpenRead(url);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}