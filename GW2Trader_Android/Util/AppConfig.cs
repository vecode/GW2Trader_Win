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
using DataLayer.Db;
using GW2Trader_Android.Util;
using DataLayer.Model;
using DataLayer.Repository;
using GW2Trader.Manager;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Util;
using GW2Trader.Util;
using GW2Trader_Android.Util.OfflineTest;

namespace GW2Trader_Android.Util
{
    public class AppConfig : IAppConfig
    {
        public void Initialize()
        {
            var container = TinyIoCContainer.Current;
            
            container.Register<IWebClientProvider, WebClientProvider>();
            container.Register<IApiAccessor, ApiAccessor>();
            container.Register<ITradingPostApiWrapper, TradingPostApiWrapper>();          

            container.Register<IDatabaseProvider, DbProvider>();            
            container.Register <ItemRepository>();

            container.Register<IItemManager, ItemManager>();

            string iconDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            container.Register<Util.IIconStore>(new Util.IconStore(iconDirectory));
        }
    }
}