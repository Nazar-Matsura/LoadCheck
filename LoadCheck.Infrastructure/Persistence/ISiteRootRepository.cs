using System.Collections.Generic;
using System.Threading.Tasks;
using LoadCheck.Core.Entities;

namespace LoadCheck.Infrastructure.Persistence
{
    public interface ISiteRootRepository : IRepository<SiteRoot>
    {
        Task<IEnumerable<SiteRoot>> GetFullAggregateSiteRoots();
    }
}