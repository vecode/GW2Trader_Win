namespace GW2Trader.Data
{
    public class GameDataContextProvider : IGameDataContextProvider
    {
        public IGameDataContext GetContext()
        {
            return new GameDataContext();
        }
    }
}