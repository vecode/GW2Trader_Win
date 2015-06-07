using GW2TPApiWrapper.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
