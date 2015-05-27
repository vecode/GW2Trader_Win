namespace GW2Trader_Windows.Data
{
    public interface IDbBuilder
    {
        void BuildDatabase();
        void RebuildDatabase();
        void UpdateDatabase();
    }
}
