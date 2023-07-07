using ABCPortfolio.Data;
using ABCPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ABCPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDBContext context,ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var projectCompletelist = _dbContext.completedProjects.ToList();
            return View(projectCompletelist);
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