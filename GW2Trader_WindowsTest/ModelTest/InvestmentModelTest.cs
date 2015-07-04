using System;
using System.Collections.Generic;
using System.Linq;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Desktop.Model;
using Xunit;
using GW2Trader_WindowsTest.Mock;
using GW2Trader_WindowsTest.TestData;

namespace GW2Trader_WindowsTest.ModelTest
{
    public class InvestmentModelTest
    {
        private InvestmentModel InvestmentModel()
        {
            return new InvestmentModel
            {
                Count = 5,
                DesiredSellPrice = 1000,
                PurchasePrice = 500,
                SoldFor = 900
            };
        }

        [Fact]
        public void PrognosedProfitShouldBeCorrect()
        {
            InvestmentModel ivm = InvestmentModel();
            int expectedProfitPerUnit = (int)(1000 * 0.85) - 500;

            Assert.Equal(expectedProfitPerUnit, ivm.PrognosedProfitPerUnit);
            Assert.Equal(expectedProfitPerUnit * 5, ivm.PrognosedTotalProfit);

            ivm.DesiredSellPrice = null;
            Assert.Equal(0, ivm.PrognosedProfitPerUnit);
        }
         
        [Fact]
        public void ActualProfitTest()
        {
            InvestmentModel ivm = InvestmentModel();
            int expectedActualProfitPerUnit = (int)(900 * 0.85) - 500;

            Assert.Equal(expectedActualProfitPerUnit, ivm.ActualProfitPerUnit);
            Assert.Equal(expectedActualProfitPerUnit * 5, ivm.ActualTotalProfit);

            ivm.SoldFor = null;
            Assert.Equal(0, ivm.ActualProfitPerUnit);
        }
    }
}
