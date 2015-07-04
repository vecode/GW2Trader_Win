using GW2Trader.ApiWrapper.Util;

namespace GW2Trader_Windows.Util
{
    public class WebClientProvider : IWebClientProvider
    {
        public IWebClient GetWebClient()
        {
            return new WinWebClient();
        }
    }
}
