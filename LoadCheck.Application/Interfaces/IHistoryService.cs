using System.Collections.Generic;
using System.Threading.Tasks;
using LoadCheck.Core.ViewModels;

namespace LoadCheck.Application.Interfaces
{
    public interface IHistoryService
    {
        Task<List<SiteTestsViewModel>> GetFullHistory();
    }
}