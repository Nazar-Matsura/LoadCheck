using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using LoadCheck.Services.Interfaces;

namespace LoadCheck.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoadCheckService _loadCheckService;

        public HomeController(ILoadCheckService loadCheckService)
        {
            _loadCheckService = loadCheckService;
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

                return PartialView(resultList);
            }
            catch (Exception ex) // Using this instead of ExceptionFilter because I couldn't figure out how to return partial view from there.
            {
                return PartialView("Error", ex);
            }
        }
    }
}