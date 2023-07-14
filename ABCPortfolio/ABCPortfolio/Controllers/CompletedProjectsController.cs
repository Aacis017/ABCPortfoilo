using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using ABCPortfolio.ViewModel;
using ABCPortfolio.Models;
using ABCPortfolio.Data;
using Microsoft.AspNetCore.Authorization;

public class CompletedProjectsController : Controller
   
{
    private readonly AppDBContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CompletedProjectsController(AppDBContext context,IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        _db = context;
    }

    // GET: CompletedProjects
    public IActionResult Index()
    {
        // Your logic to fetch and display existing completed projects
        return View();
    }

    // GET: CompletedProjects/Details/5
    public IActionResult Details(int? id)
    {
        var project = _db.completedProjects.Find(id);
        if (id == null)
        {
            return NotFound();
        }

        // Fetch the existing completed project by id from your database
        // Example: var project = await _context.CompletedProjects.FindAsync(id);
        // Example: if (project == null) return NotFound();

        var model = new CompletedProjectsVM
        {
            //Id = project.Id,
            Title = project.Title,
            Description = project.Description
        };

        return View(model);
    }

    [Authorize]
    // GET: CompletedProjects/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CompletedProjects/Create
    [Authorize]

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CompletedProjectsVM model)
    {
        if (ModelState.IsValid)
        {
            if (model.Imageforproject != null && model.Imageforproject.Length > 0)
            {
                //var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "/wwwroot/ProjectsPhotos");
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ProjectsPhotos");
                Directory.CreateDirectory(uploadsFolder);
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Imageforproject.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Imageforproject.CopyToAsync(fileStream);
                }

                var completedProject = new CompletedProject()
                {
                    Imageforproject = uniqueFileName,
                    Description = model.Description,
                    Title = model.Title
                };

                _db.completedProjects.Add(completedProject);
                _db.SaveChanges();
             



                // Save the uniqueFileName to your database or perform additional logic
            }

            // Save the remaining properties of the model to your database or perform additional logic
            // Example: _context.CompletedProjects.Add(model);
            // Example: await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GetProjects));
        }

        return View(model);
    }

    // GET: CompletedProjects/Edit/5
    public IActionResult Edit(int? id)
    {
        var project = _db.completedProjects.Find(id);
        if (id == null)
        {
            return NotFound();
        }

        // Fetch the existing completed project by id from your database
        // Example: var project = await _context.CompletedProjects.FindAsync(id);
        // Example: if (project == null) return NotFound();

        var model = new CompletedProjectsVM
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description
        };

        return View(model);
    }

    // POST: CompletedProjects/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CompletedProjectsVM model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (model.Imageforproject != null && model.Imageforproject.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ProjectsPhotos");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Imageforproject.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Imageforproject.CopyToAsync(fileStream);
                }
                // Update the uniqueFileName in your database or perform additional logic
            }

            // Update the remaining properties of the model in your database or perform additional logic
            // Example: _context.Update(model);
            // Example: await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    // GET: CompletedProjects/Delete/5
    public IActionResult Delete(int? id)
    {
        var project = _db.completedProjects.Find(id);
        if (id == null)
        {
            return NotFound();
        }

        // Fetch the existing completed project by id from your database
        // Example: var project = await _context.CompletedProjects.FindAsync(id);
        // Example: if (project == null) return NotFound();

        var model = new CompletedProjectsVM
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description
        };

        return View(model);
    }

    // POST: CompletedProjects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        // Delete the completed project from your database
        // Example: var project = await _context.CompletedProjects.FindAsync(id);
        // Example: if (project != null) _context.CompletedProjects.Remove(project);
        // Example: await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult GetProjects()
    {
        var projects = _db.completedProjects.ToList();
        return View(projects);
       
    }
}

//using ABCPortfolio.Data;
//using ABCPortfolio.Services;
//using ABCPortfolio.ViewModel;
//using Microsoft.AspNetCore.Mvc;

//namespace ABCPortfolio.Controllers
//{
//    public class CompletedProjectsController : Controller
//    {
//        private readonly ICompletedProjectService _service;


//        public CompletedProjectsController(ICompletedProjectService service)
//        {
//            _service = service;


//        }
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet]

//        public IActionResult Create()
//        {
//            return View();

//        }
//        public IActionResult Create(CompletedProjectsVM projectInformation) 
//        {
//            if(ModelState.IsValid)
//            {
//                _service.Create(projectInformation);
//                return RedirectToAction("Index");
//            }
//            return View(projectInformation);
//        }
//        [HttpGet]
//        public IActionResult Edit()
//        {

//            return View();
//        }
//        [HttpPost]
//        public IActionResult Edit(CompletedProjectsVM projectInformation)
//        {

//            var editResult = _service.Update(projectInformation);
//            return RedirectToAction("Index");
//        }
//        //public IActionResult Delete(int? id)
//        //{
//        //    if (id == null || _context.Users == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    var item = _context.Users.Find(id);
//        //    if (item == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    return View(item);

//        //}
//        [HttpPost, ActionName("Delete")]
//        public IActionResult Delete(int id)
//        {
//            var del = _service.Delete(id);
//            if (del)
//            {
//                return RedirectToAction("Index");
//            }
//            //var item = _context.Users.Find(id);
//            //if (item != null){
//            //    _context.Remove(item);
//            //}
//            //_context.SaveChanges();
//            return View();
//        }




//        public IActionResult Detalis(int id)
//        {
//            var data = _service.Details(id);
//            return View(data);


//        }
//    }


//}





