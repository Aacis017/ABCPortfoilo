using ABCPortfolio.Data;
using ABCPortfolio.Services.IServices;
using ABCPortfolio.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABCPortfolio.Controllers
{
    public class CareerController : Controller
    {
        private readonly ICareerServices _service;
        public CareerController(ICareerServices service)
        {
            _service = service;       
        }

        [Authorize]
        public IActionResult Create()
        {
            var jobData = _service.GetJobs();
            var model = new ParentVM()
            {
                requirement = null,
                career=null,
                job = jobData
            };
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(ParentVM jobData)
        {
            _service.AddJob(jobData.career);
            return RedirectToAction("Create");
        }
        public IActionResult Index()
        {
            var Careerdata = _service.GetJobs();
            return View(Careerdata);
        }
        [HttpPost]
        public IActionResult AddRequirement(int id, ParentVM jobData)
        {
            _service.AddRequirement(id,jobData.requirement);
            return RedirectToAction("Create");
        }
        [HttpGet]
        public IActionResult DeleteRequirement(int id)
        {
            _service.DeleteRequirement(id);
            return RedirectToAction("Create");
        }
        [HttpGet]
        public IActionResult DeleteJob(int id)
        {
            _service.DeleteJob(id);
            return RedirectToAction("Create");
        }
    }
    
}
