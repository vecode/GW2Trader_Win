namespace GW2Trader.Desktop.Data
{
    public class GameDataContextProvider : IGameDataContextProvider
    {
        public IGameDataContext GetContext()
        {
            return new GameDataContext();
        }
    }
}