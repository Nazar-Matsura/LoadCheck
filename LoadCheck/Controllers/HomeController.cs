using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using LoadCheck.Application.Interfaces;

namespace LoadCheck.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoadCheckService _loadCheckService;
        private readonly IPersistenceService _persistenceService;
        private readonly IHistoryService _historyService;

        public HomeController(ILoadCheckService loadCheckService,
            IPersistenceService persistenceService,
            IHistoryService historyService)
        {
            _loadCheckService = loadCheckService;
            _persistenceService = persistenceService;
            _historyService = historyService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CheckUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                var resultList = await _loadCheckService.CheckLoadAt(uri);

                await _persistenceService.SaveTestResults(uri, resultList);

                return PartialView(resultList);
            }
            catch (Exception ex) // Using this instead of ExceptionFilter because I couldn't figure out how to return partial view from there.
            {
                return PartialView("Error", ex);
            }
        }

        public async Task<ActionResult> History()
        {
            return View(await _historyService.GetFullHistory());
        }
    }
}