using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;

namespace GW2Trader.Data
{
    interface IApiDataUpdater
    {
        void UpdateItemInformation(GameItemModel item);
        void UpdateCommerceData(GameItemModel item);
    }
}
