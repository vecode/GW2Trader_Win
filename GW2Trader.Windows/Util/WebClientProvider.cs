using GW2Trader.ApiWrapper.Util;

namespace GW2Trader.Desktop.Util
{
    public class WebClientProvider : IWebClientProvider
    {
        public IWebClient GetWebClient()
        {
            return new WinWebClient();
        }
    }
}
