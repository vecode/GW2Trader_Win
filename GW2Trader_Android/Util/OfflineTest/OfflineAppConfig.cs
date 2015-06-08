using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TinyIoC;
using GW2TPApiWrapper.Wrapper;
using DataLayer.Db;
using DataLayer.Repository;
using GW2Trader.Manager;

namespace GW2Trader_Android.Util.OfflineTest
{
    public class OfflineAppConfig : IAppConfig
    {
        public void Initialize()
        {
            var container = TinyIoCContainer.Current;
            
            container.Register<ITradingPostApiWrapper, OfflineTPWrapperMock>();

            container.Register<IDatabaseProvider, DbProvider>();
            container.Register<ItemRepository>();

            container.Register<IItemManager, ItemManager>();
        }
    }
}