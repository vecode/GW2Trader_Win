using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Data
{
    public interface IDbBuilder
    {
        void UpdateDatabase();
        void BuildDatabase();
        void RebuildDatabase();
    }
}
