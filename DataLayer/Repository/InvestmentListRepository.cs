using DataLayer.Model;
using GW2Trader.Data.Db;

namespace DataLayer.Repository
{
    public class InvestmentListRepository : Repository<InvestmentList>
    {
        public InvestmentListRepository(IDatabaseProvider dbProvider)
            :base(dbProvider){ }
    }
}
