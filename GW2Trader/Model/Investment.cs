using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Model
{
    public class Investment : DataLayer.Model.Investment
    {
        // TODO replace dummy getters
        public int CurrentProfitPerUnit
        {
            get { return 0; }
        }

        public int CurrentTotalProfil
        {
            get { return 0; }
        }
    }
}
