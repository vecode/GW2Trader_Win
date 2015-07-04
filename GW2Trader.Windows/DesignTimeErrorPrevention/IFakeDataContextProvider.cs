using GW2Trader.Desktop.Data;

namespace GW2Trader.Desktop.DesignTimeErrorPrevention
{
    public class FakeDataContextProvider : IGameDataContextProvider
    {
        public IGameDataContext GetContext()
        {
            return new DesignTimeGameDataContext();
        }
    }
}
