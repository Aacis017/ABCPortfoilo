using ABCPortfolio.Data;
using ABCPortfolio.Services.IServices;
using ABCPortfolio.ViewModel;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace ABCPortfolio.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IserviceServices _service;
        private readonly AppDBContext _context;
        public ServicesController(IserviceServices service, AppDBContext context)
        {
            _service = service;
            _context = context;
        }
        public IActionResult Index()
        {
            var items = _service.GetServices();
            return View(items);
        }   
        public IActionResult Create()
        {
            var model = new ParentServiceVM()
            {
                createService = null,
                getService = _service.GetServices()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ParentServiceVM service)
        {
            _service.AddServices(service.createService);
            return RedirectToAction("Create","Services");
        }
  
        public IActionResult DeleteService(int id)
        {
            _service.DeleteServices(id);
            return RedirectToAction("Create","Services");

        }
        public IActionResult Update(int id)
        {
            var service = _context.Services.FirstOrDefault(u => u.Id == id);
            var serviceCreateVM = new ServiceCreateVM
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description
            };

         
            return View(serviceCreateVM);
        }

        [HttpPost]
        public IActionResult Update(ServiceCreateVM service)
        {
            _service.UpdateService(service);
            return RedirectToAction("Create","Services");
        }
    }
}

