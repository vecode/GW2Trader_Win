using System.IO;
using Android.OS;
using DataLayer.Repository;
using GW2Trader.ApiWrapper.Util;
using GW2Trader.ApiWrapper.Wrapper;
using GW2Trader.Data.Db;
using GW2Trader.Manager;
using TinyIoC;

namespace GW2Trader.Android.Util
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
            container.Register<ItemRepository>();

            container.Register<IItemManager, ItemManager>();

            string iconDirectory = Path.Combine(Environment.ExternalStorageDirectory.AbsolutePath, "GW2Trader", "Icons");
            Directory.CreateDirectory(iconDirectory);
            container.Register<IIconStore>(new IconStore(iconDirectory));
        }
    }
}