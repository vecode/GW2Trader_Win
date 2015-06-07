using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Util
{
    public interface IWebClientProvider
    {
        IWebClient GetWebClient();
    }
}
