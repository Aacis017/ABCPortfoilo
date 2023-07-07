using ABCPortfolio.Data;
using ABCPortfolio.Models;
using ABCPortfolio.Services.IServices;
using ABCPortfolio.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ABCPortfolio.Services
{
    public class ServiceServices : IserviceServices
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly AppDBContext _context;
        public ServiceServices(AppDBContext context, IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public bool AddServices(ServiceCreateVM service)
        {
            string? fileName = null;
            if(service.Images != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/img/ServiceImage");
                fileName = Guid.NewGuid().ToString() + "_" + service.Images.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                    var fsStream = new FileStream(filePath, FileMode.Create);
                service.Images.CopyTo(fsStream);
                fsStream.Dispose();
            }
            var newservice = new Service()
            {
                Id =service.Id,
                Title=service.Title,
                Description =service.Description,
                Images = fileName
            };
            _context.Services.Add(newservice);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteServices(int id)
        {
            var ServiceItem = _context.Services.FirstOrDefault(item => item.Id == id);
            _context.Services.Remove(ServiceItem);
            _context.SaveChanges();
            return true;
        }
        public List<ServiceVM> GetServices()
        {
            var servicesData = _context.Services.ToList();
            var data = (from s in servicesData
                        select new ServiceVM()
                        {   Id=s.Id,
                            Title = s.Title,
                            Description = s.Description,
                            ImageName = s.Images
                        }).ToList() ;
            return data;
        }

        //public bool UpdateService(ServiceCreateVM service)
        //{

        //    var Existingservice = _context.Services.FirstOrDefault(e => e.Id == service.Id);

        //    Existingservice.Title = service.Title;
        //    Existingservice.Description = service.Description;
        //    string? uniquefileName = null;
        //    if(service.Images != null)
        //    {
        //        string uploadFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/img/ServiceImage");
        //        var  fileName = Guid.NewGuid().ToString() + "_" + service.Images.FileName;
        //        string filePath = Path.Combine(uploadFolder, fileName);

        //        if (System.IO.File.Exists(filePath))
        //        {
        //            System.IO.File.Delete(filePath);
        //        }
        //        using (var fsStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            service.Images.CopyTo(fsStream);
        //        }
        //        var newservice = new Service()
        //        {
        //            Id = service.Id,
        //            Title = service.Title,
        //            Description = service.Description,
        //            Images = fileName
        //        };
        //        _context.Services.Update(newservice);
        //        _context.SaveChanges();
        //    }
        //    return true;
        //}


        public bool UpdateService(ServiceCreateVM service)
        {
            var existingService = _context.Services.FirstOrDefault(e => e.Id == service.Id);

            if (existingService == null)
            {
                // Service not found, return false or handle the scenario accordingly
                return false;
            }


            existingService.Title = service.Title;
            existingService.Description = service.Description;
            string old_image = existingService.Images;

            if (service.Images != null)
            {
                string uploadFolder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/img/ServiceImage");
                var fileName = Guid.NewGuid().ToString() + "_" + service.Images.FileName;
                string filePath = Path.Combine(uploadFolder, fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                using (var fsStream = new FileStream(filePath, FileMode.Create))
                {
                    service.Images.CopyTo(fsStream);
                }

                existingService.Images = fileName;
            }
            else
            {
                existingService.Images = old_image;
            }

            _context.SaveChanges();

            return true;
        }

    }
}
