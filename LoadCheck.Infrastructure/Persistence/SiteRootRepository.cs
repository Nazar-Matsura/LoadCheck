using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LoadCheck.Core.Entities;

namespace LoadCheck.Infrastructure.Persistence
{
    public class SiteRootRepository : Repository<SiteRoot>, ISiteRootRepository
    {
        public SiteRootRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<SiteRoot>> GetFullAggregateSiteRoots()
        {
            return _entities
                .AsQueryable()
                .Include(r => r.Tests.Select(t => t.TestResults));
        }
    }
}