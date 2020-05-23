using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using LoadCheck.Models;
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

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> CheckUri(string url)
        {
            var uri = new Uri(url);
            var resultList = await _loadCheckService.CheckLoadAt(uri);

            var chart = ResponseMaxTimeChart(resultList);

            return PartialView(new CheckUrlResults{ChartImage = chart, Data = resultList});
        }

        private byte[] ResponseMaxTimeChart(List<UrlResponseTimes> data)
        {
            // For some reason, bar chart displays first items on the bottom.
            var reversedData = data.Select(t => t).Reverse().ToList();
            var urls = reversedData.Select(r => r.Url.ToString()).ToList();
            var maxTimes = reversedData.Select(r => r.MaxResponseTime).ToList();

            var chart = new Chart(1000, 500)
                .AddTitle("Response max time")
                .AddSeries(xValue: urls, yValues: maxTimes, chartType: "Bar");

            return chart.GetBytes();
        }
    }
}