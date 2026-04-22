using BLL.Interfaces;
using GymManagementSolution_PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymManagementSolution_PL.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger _logger;
        private readonly IAnalyticsService _analyticsService;

        public HomeController(IAnalyticsService analyticsService)
        {
            //_logger = logger;
            _analyticsService = analyticsService;
        }
        public IActionResult Index()
        {
            var Data = _analyticsService.GetAnalyticsData();
            return View(Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
