using DataLayer.Repository;
using GW2Trader.ApiWrapper.Wrapper;
using GW2Trader.Data.Db;
using GW2Trader.Manager;
using TinyIoC;

namespace GW2Trader.Android.Util.OfflineTest
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