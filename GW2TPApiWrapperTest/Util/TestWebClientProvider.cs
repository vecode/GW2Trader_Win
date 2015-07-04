using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GW2Trader.ApiWrapper.Util;

namespace GW2TPApiWrapperTest.Util
{
    class TestWebClientProvider : IWebClientProvider
    {
        public IWebClient GetWebClient()
        {
            return new TestWebClient();
        }
    }
}
