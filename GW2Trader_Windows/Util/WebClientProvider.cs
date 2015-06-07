using GW2TPApiWrapper.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
