using GW2Trader.ApiWrapper.Util;

namespace GW2Trader.Android.Util
{
    public class WebClientProvider : IWebClientProvider
    {
        public IWebClient GetWebClient()
        {
            return new AndroidWebClient();
        }
    }
}