using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader_Windows.Extension
{
    public static class StringExtension
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        {
            if (value == null)
            {
                return true;
            }

            value = value.Trim();
            return string.IsNullOrEmpty(value);
        }
    }
}
