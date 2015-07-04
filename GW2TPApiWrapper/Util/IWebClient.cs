using System;
using System.IO;

namespace GW2Trader.ApiWrapper.Util
{
    public interface IWebClient : IDisposable
    {
        Stream OpenRead(string url);
    }
}
