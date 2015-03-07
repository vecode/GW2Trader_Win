using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Data
{
    public class GameDataContextProvider : IGameDataContextProvider
    {
        public IGameDataContext GetContext()
        {
            return new GameDataContext();
        }
    }
}
