using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadCheck.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<List<SiteTestsViewModel>> GetFullHistory();
    }
}