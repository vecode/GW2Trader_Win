using DataLayer.Db;
using DataLayer.Model;

namespace DataLayer.Repository
{
    public class InvestmentRepository : Repository<Investment>
    {
        public InvestmentRepository(IDatabaseProvider dbProvider)
            : base(dbProvider) { }
    }
}
