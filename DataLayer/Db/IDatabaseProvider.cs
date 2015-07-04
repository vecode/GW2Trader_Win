namespace GW2Trader.Data.Db
{
    public interface IDatabaseProvider
    {
        /// <returns>Returns an instance of the Database class</returns>
        Database GetDatabase();
    }
}
