using DataLayer.Db;
using DataLayer.Model;

namespace DataLayer.Repository
{
    public class InvestmentListRepository : GenericRepository<InvestmentList>
    {
        public InvestmentListRepository(IDatabaseProvider dbProvider)
            :base(dbProvider){ }
    }
}
