using DataLayer.Db;
using DataLayer.Model;

namespace DataLayer.Repository
{
    public class IconRepository : GenericRepository<Icon>
    {
        public IconRepository(IDatabaseProvider dbProvider)
            : base(dbProvider) { }
    }
}
