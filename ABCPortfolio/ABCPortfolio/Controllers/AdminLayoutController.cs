using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABCPortfolio.Controllers
{
    public class AdminLayoutController : Controller
    {

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
