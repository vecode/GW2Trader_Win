namespace GW2Trader.Data
{
    public interface IDbBuilder
    {
        void BuildDatabase();
        void RebuildDatabase();
        void UpdateDatabase();
    }
}
