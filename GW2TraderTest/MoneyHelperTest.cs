using GW2Trader.Util;
using System;
using System.Linq;
using Xunit;

namespace GW2TraderTest
{
    public class MoneyHelperTest
    {
        [Fact]
        public void ExtractCopperTest()
        {
            Assert.Equal(56, MoneyHelper.ExtractPositivCopperShare(123456));
            Assert.Equal(56, MoneyHelper.ExtractPositivCopperShare(-123456));
        }

        [Fact]
        public void ExtractSilverTest()
        {
            Assert.Equal(34, MoneyHelper.ExtractPositivSilverShare(123456));
            Assert.Equal(34, MoneyHelper.ExtractPositivSilverShare(-123456));
        }

        [Fact]
        public void ExtractGoldTest()
        {
            Assert.Equal(123, MoneyHelper.ExtractPositivGoldShare(1234567));
            Assert.Equal(123, MoneyHelper.ExtractPositivGoldShare(-1234567));
        }
    }
}
