using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Util
{
    public static class MoneyHelper
    {
        public static int ExtractCopperShare(int value)
        {
            value = Math.Abs(value);
            return value < 100 ? value : value % 100;
        }

        public static int ExtractSilverShare(int value)
        {
            value = Math.Abs(value);

            if (value < 10000)
            {
                return value / 100;
            }

            value = value % 10000;
            return value / 100;
        }

        public static int ExtractGoldShare(int value)
        {
            value = Math.Abs(value);
            return value / 10000;
        }
    }
}
