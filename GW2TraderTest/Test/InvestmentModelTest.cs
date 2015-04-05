using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GW2Trader.Model;


namespace GW2TraderTest
{
    [TestClass]
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

        [TestMethod]
        public void PrognosedProfitTest()
        {
            InvestmentModel ivm = InvestmentModel();
            int expectedProfitPerUnit = (int)(1000 * 0.85) - 500;

            Assert.AreEqual(expectedProfitPerUnit, ivm.PrognosedProfitPerUnit);
            Assert.AreEqual(expectedProfitPerUnit * 5, ivm.PrognosedTotalProfit);

            ivm.DesiredSellPrice = null;
            Assert.AreEqual(0, ivm.PrognosedProfitPerUnit);
        }

        [TestMethod]
        public void ActualProfitTest()
        {
            InvestmentModel ivm = InvestmentModel();
            int expectedActualProfitPerUnit = (int)(900 * 0.85) - 500;

            Assert.AreEqual(expectedActualProfitPerUnit, ivm.ActualProfitPerUnit);
            Assert.AreEqual(expectedActualProfitPerUnit * 5, ivm.ActualTotalProfit);

            ivm.SoldFor = null;
            Assert.AreEqual(0, ivm.ActualProfitPerUnit);
        }
    }
}
