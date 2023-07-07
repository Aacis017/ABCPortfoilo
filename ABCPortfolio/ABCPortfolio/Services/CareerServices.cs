using ABCPortfolio.Data;
using ABCPortfolio.Models;
using ABCPortfolio.Services.IServices;
using ABCPortfolio.ViewModel;
using Microsoft.Identity.Client;

namespace ABCPortfolio.Services
{
    public class CareerServices : ICareerServices,IDisposable
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly AppDBContext _context;
        public CareerServices(AppDBContext context , IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public bool AddJob(CareerVM model)
        {
            string? fileName = null;
            if(model.CreateImage != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/JobImage");
                fileName = Guid.NewGuid().ToString() + "_" + model.CreateImage.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                var fsStream = new FileStream(filePath, FileMode.Create);
                model.CreateImage.CopyTo(fsStream);
                fsStream.Dispose();
            }
            var newJob = new Job()
            {
                Id = 0,
                ImagePath = fileName,
                Title = model.Title
            };
            _context.Jobs.Add(newJob);
            _context.SaveChanges();
            return true;
        }

        public bool AddRequirement(int id, string requirement)
        {
            var newRequirement = new Requirement()
            {
                Id = 0,
                JobId = id,
                Qualification = requirement
            };
            _context.Requirements.Add(newRequirement);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteRequirement(int id)
        {
            var dataToDelete = _context.Requirements.Find(id);
            _context.Requirements.Remove(dataToDelete);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteJob(int id)
        {
            var Job = _context.Jobs.Find(id);
            if (Job.ImagePath != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/JobImage");
                string existingFilePath = Path.Combine(uploadsFolder, Job.ImagePath);
                if (File.Exists(existingFilePath))
                {
                    File.Delete(existingFilePath);
                }
            }
            var reqList = _context.Requirements.Where(x => x.JobId == id);
            _context.Requirements.RemoveRange(reqList);
            _context.SaveChanges();
            _context.Jobs.Remove(Job);
            _context.SaveChanges();
            return true;
        }

        public IQueryable<JobVM> GetJobs()
        {
            var jobData = _context.Jobs.ToList();
            var careerList = (from c in jobData
                              select new JobVM()
                              {
                                  Id = c.Id,
                                  ImagePath = c.ImagePath,
                                  Title = c.Title,
                                  Qualification = (from x in _context.Requirements.Where(x => x.JobId == c.Id).ToList()
                                                   select new QualificationVM()
                                                   {
                                                     Id = x.Id,
                                                     QualificationText = x.Qualification
                                                   }).AsQueryable()

                              }).AsQueryable();
            return careerList;
        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }
    }
}
