using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapper.Util
{
    public interface IWebClient : IDisposable
    {
        Stream OpenRead(string url);
    }
}
