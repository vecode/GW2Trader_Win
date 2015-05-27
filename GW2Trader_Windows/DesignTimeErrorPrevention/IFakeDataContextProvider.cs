using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.Data;

namespace GW2Trader_Windows.DesignTimeErrorPrevention
{
    public class FakeDataContextProvider : IGameDataContextProvider
    {
        public IGameDataContext GetContext()
        {
            return new DesignTimeGameDataContext();
        }
    }
}
