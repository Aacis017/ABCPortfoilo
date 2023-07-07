using ABCPortfolio.Data;
using ABCPortfolio.Models;
using ABCPortfolio.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ABCPortfolio.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly AppDBContext _AppDBContext;
        public PortfolioController(AppDBContext AppDBContext)
        {
            _AppDBContext = AppDBContext;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateProjects()
        {
            return View();
        }
        public IActionResult AddProjectDetails(Projects projects)
        {
            _AppDBContext.Projects.Add(projects);
            _AppDBContext.SaveChanges();
            return RedirectToAction("CreateProjects");
        }
        public IActionResult GetAllProjects()


        {
            var model = new AboutUsViewModel()
            {
                projects = _AppDBContext.Projects.First(),
                teams = _AppDBContext.Teams.ToList()

            };

            return View(model);
        }
        public IActionResult GetProjectsById(int Id)
        {

            Projects projects = _AppDBContext.Projects.FirstOrDefault(p => p.id == Id);
            return View(projects);
        }

        public IActionResult UpdateProjectsById(Projects projects)
        {
            _AppDBContext.Projects.Update(projects);
            _AppDBContext.SaveChanges();
            return RedirectToAction("GetAllProjects");

        }
        public IActionResult RemoveProjectsById(Projects projects)
        {
            _AppDBContext.Projects.Remove(projects);
            _AppDBContext.SaveChanges();
            return RedirectToAction("GetAllProjects");

        }

        public IActionResult GetAllProjectsUser()
        {
            var team = _AppDBContext.Teams.ToList();
            var project = _AppDBContext.Projects.FirstOrDefault();
            var model = new AboutUsViewModel()
            {
                projects = project,
                teams = team
            };
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
    

