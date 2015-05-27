using DataLayer.Db;
using DataLayer.Model;

namespace DataLayer.Repository
{
    public class InvestmentRepository : GenericRepository<Investment>
    {
        public InvestmentRepository(IDatabaseProvider dbProvider)
            : base(dbProvider) { }
    }
}
