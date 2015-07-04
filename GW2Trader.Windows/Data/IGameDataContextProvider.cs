namespace GW2Trader.Desktop.Data
{
    public interface IGameDataContextProvider
    {
        IGameDataContext GetContext();
    }
}
