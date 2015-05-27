using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader_Windows.Data
{
    public interface IGameDataContextProvider
    {
        IGameDataContext GetContext();
    }
}
