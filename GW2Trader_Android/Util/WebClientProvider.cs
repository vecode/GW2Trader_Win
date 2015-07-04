using GW2Trader.ApiWrapper.Util;

namespace GW2Trader_Android.Util
{
    public class WebClientProvider : IWebClientProvider
    {
        public IWebClient GetWebClient()
        {
            return new AndroidWebClient();
        }
    }
}
