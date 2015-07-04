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
            Assert.Equal(56, MoneyHelper.ExtractCopperShare(123456));
            Assert.Equal(56, MoneyHelper.ExtractCopperShare(-123456));
        }

        [Fact]
        public void ExtractSilverTest()
        {
            Assert.Equal(34, MoneyHelper.ExtractSilverShare(123456));
            Assert.Equal(34, MoneyHelper.ExtractSilverShare(-123456));
        }

        [Fact]
        public void ExtractGoldTest()
        {
            Assert.Equal(123, MoneyHelper.ExtractGoldShare(1234567));
            Assert.Equal(123, MoneyHelper.ExtractGoldShare(-1234567));
        }
    }
}
