namespace GW2Trader_Windows.Data
{
    public class GameDataContextProvider : IGameDataContextProvider
    {
        public IGameDataContext GetContext()
        {
            return new GameDataContext();
        }
    }
}