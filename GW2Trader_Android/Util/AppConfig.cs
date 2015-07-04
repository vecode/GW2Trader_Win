using TinyIoC;
using DataLayer.Repository;
using GW2Trader.Manager;
using GW2Trader.ApiWrapper.Wrapper;
using GW2Trader.ApiWrapper.Util;
using GW2Trader.Data.Db;

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

            string iconDirectory = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "GW2Trader", "Icons");
			System.IO.Directory.CreateDirectory (iconDirectory);
            container.Register<Util.IIconStore>(new Util.IconStore(iconDirectory));
        }
    }
}