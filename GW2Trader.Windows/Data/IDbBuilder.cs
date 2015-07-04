namespace GW2Trader.Desktop.Data
{
    public interface IDbBuilder
    {
        void BuildDatabase();
        void RebuildDatabase();
        void UpdateDatabase();
    }
}
