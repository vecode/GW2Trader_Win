using TinyIoC;
using DataLayer.Db;
using DataLayer.Repository;
using GW2Trader.Manager;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Util;

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
            //container.Register<Util.IIconStore>(new Util.IconStore2(iconDirectory));
            container.Register<Util.IIconStore>(new Util.IconStore(iconDirectory));
            //container.Register<Util.IIconStoreBetter>(new Util.IconStore3(iconDirectory));
        }
    }
}