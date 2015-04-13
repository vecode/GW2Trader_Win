﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;

namespace GW2TraderTestxUnit.TestData
{
    internal interface ITestDataFactory
    {
        IEnumerable<GameItemModel> GetTestGameItems();
    }
}
