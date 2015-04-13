﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Service;

namespace GW2TraderTestxUnit.Mock
{
    internal class NotifyServiceMock : INotifyService
    {
        public bool IsNotified { get; set; }

        public void Notify(string message)
        {
            IsNotified = true;
        }
    }
}
