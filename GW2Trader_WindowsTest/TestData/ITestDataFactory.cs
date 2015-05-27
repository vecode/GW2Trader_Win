using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.Model;

namespace GW2Trader_WindowsTest.TestData
{
    internal interface ITestDataFactory
    {
        IEnumerable<GameItemModel> GetTestGameItems();
    }
}
