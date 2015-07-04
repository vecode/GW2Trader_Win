using DataLayer.Model;
using GW2Trader.Data.Db;

namespace DataLayer.Repository
{
    public class InvestmentRepository : Repository<Investment>
    {
        public InvestmentRepository(IDatabaseProvider dbProvider)
            : base(dbProvider) { }
    }
}
