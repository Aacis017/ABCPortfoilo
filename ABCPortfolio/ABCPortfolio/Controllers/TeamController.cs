
using ABCPortfolio.Data;
using ABCPortfolio.Models;
using ABCPortfolio.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ABCPortfolio.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDBContext _AppDBContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TeamController(AppDBContext AppDBContext, IWebHostEnvironment hostEnvironment)
        {
            _AppDBContext = AppDBContext;
            _hostEnvironment = hostEnvironment;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> AddTeamDetails(TeamViewModel model)
        //{
        //    string uniqueFileName = null;
        //    if (model.ImagePath != null)
        //    {
        //        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Upload");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await model.ImagePath.CopyToAsync(fileStream);
        //        }
        //    }

        //    var newTeam = new Team()
        //    {
        //        ImagePath = uniqueFileName,
        //        Id = 0,
        //        Name = model.Name,
        //        Post = model.Post
        //    };

        //    await _AppDBContext.Teams.AddAsync(newTeam);
        //    await _AppDBContext.SaveChangesAsync();

        //    return RedirectToAction("GetAllTeam");
        //}
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(TeamViewModel model)
        {
            string uniqueFileName = null;
            if (model.ImagePath != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Upload");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImagePath.CopyToAsync(fileStream);
                }
            }

            var newTeam = new Team()
            {
                ImagePath = uniqueFileName,
                Name = model.Name,
                Post = model.Post
            };

            _AppDBContext.Teams.Add(newTeam);
            _AppDBContext.SaveChanges();

            return RedirectToAction("GetAllTeam");
        }

        //public IActionResult GetAllTeam()
        //{
        //    var teams = _AppDBContext.Teams.ToList();
        //    return View(teams);
        //}
        public async Task<IActionResult> GetAllTeam()
        {
            var teams = await _AppDBContext.Teams.ToListAsync();
            return View(teams);
        }


        //[Authorize]
        //public IActionResult RemoveTeamById(Team team)
        //{
        //    _AppDBContext.Teams.Remove(team);
        //    _AppDBContext.SaveChanges();
        //    return RedirectToAction("GetAllTeam");
        //}
        [Authorize]
        public IActionResult RemoveTeamById(int id)
        {
            var team = _AppDBContext.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            _AppDBContext.Teams.Remove(team);
            _AppDBContext.SaveChanges();

            return RedirectToAction("GetAllTeam");
        }

        [HttpGet]
        
        public IActionResult GetTeamById(int id)
        {
            var team = _AppDBContext.Teams.FirstOrDefault(t => t.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            var updateViewModel = new UpdateTeamViewModel()
            {
                Id = team.Id,
                Name = team.Name,
                Post = team.Post,
                ExistingImagePath = team.ImagePath
            };

            return View(updateViewModel);
        }



        //[HttpPost]
        //[Authorize]
        //public IActionResult UpdateTeamById(UpdateTeamViewModel model)
        //{
        //    model.ExistingImagePath = _AppDBContext.Teams.Find(model.Id).ImagePath;
        //    if (ModelState.IsValid)
        //    {
        //        var existingTeam = _AppDBContext.Teams.FirstOrDefault(t => t.Id == model.Id);
        //        if (existingTeam != null)
        //        {
        //            existingTeam.Name = model.Name;
        //            existingTeam.Post = model.Post;


        //            string? uniqueFileName = null;
        //            if (model.NewImage != null)
        //            {
        //                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Upload");
        //                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.NewImage.FileName;
        //                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //                using (var fileStream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    model.NewImage.CopyTo(fileStream);
        //                }

        //                string existingFilePath = Path.Combine(uploadsFolder, existingTeam.ImagePath);
        //                if (System.IO.File.Exists(existingFilePath))
        //                {
        //                    System.IO.File.Delete(existingFilePath);
        //                }

        //                existingTeam.ImagePath = uniqueFileName;
        //            }
        //            existingTeam.ImagePath = uniqueFileName;
        //            _AppDBContext.SaveChanges();

        //            return RedirectToAction("GetAllTeam");
        //        }
        //    }

        //    var team = _AppDBContext.Teams.FirstOrDefault(t => t.Id == model.Id);
        //    if (team == null)
        //    {
        //        return NotFound();
        //    }

        //    model.ExistingImagePath = team.ImagePath;
        //    return View("GetTeamById", model);
        //}
        //[HttpPost]
        //[Authorize]
        //public IActionResult UpdateTeamById(UpdateTeamViewModel model)
        //{
        //    var existingTeam = _AppDBContext.Teams.FirstOrDefault(t => t.Id == model.Id);
        //    if (existingTeam == null)
        //    {
        //        return NotFound();
        //    }

        //    // Store the existing image path
        //    string existingImagePath = existingTeam.ImagePath;

        //    if (ModelState.IsValid)
        //    {
        //        existingTeam.Name = model.Name;
        //        existingTeam.Post = model.Post;

        //        if (model.NewImage != null)
        //        {
        //            // Delete the existing image if a new image is uploaded
        //            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Upload");
        //            string existingFilePath = Path.Combine(uploadsFolder, existingImagePath);

        //            if (System.IO.File.Exists(existingFilePath))
        //            {
        //                System.IO.File.Delete(existingFilePath);
        //            }

        //            // Save the new image
        //            string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.NewImage.FileName;
        //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                model.NewImage.CopyTo(fileStream);
        //            }

        //            // Update the image path in the existing team
        //            existingTeam.ImagePath = uniqueFileName;
        //        }
        //        else
        //        {
        //            // If no new image is uploaded, retain the existing image path
        //            existingTeam.ImagePath = existingImagePath;
        //        }

        //        _AppDBContext.SaveChanges();

        //        return RedirectToAction("GetAllTeam");
        //    }

        //    // If the model state is not valid, return the view with the existing image path
        //    model.ExistingImagePath = existingImagePath;
        //    return View("GetTeamById", model);
        //}
        [HttpPost]
        [Authorize]
        public IActionResult UpdateTeamById(UpdateTeamViewModel model)
        {
            var existingTeam = _AppDBContext.Teams.FirstOrDefault(t => t.Id == model.Id);
            if (existingTeam == null)
            {
                return NotFound();
            }

            // Store the existing image path
            string existingImagePath = existingTeam.ImagePath;

            if (ModelState.IsValid)
            {
                existingTeam.Name = model.Name;
                existingTeam.Post = model.Post;

                if (model.NewImage != null)
                {
                    // Delete the existing image if a new image is uploaded
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Upload");
                    string existingFilePath = Path.Combine(uploadsFolder, existingImagePath);

                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath);
                    }

                    // Save the new image
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.NewImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.NewImage.CopyTo(fileStream);
                    }

                    // Update the image path in the existing team
                    existingTeam.ImagePath = uniqueFileName;
                }
                else
                {
                    // If no new image is uploaded, retain the existing image path
                    existingTeam.ImagePath = existingImagePath;
                }

                _AppDBContext.SaveChanges();

                return RedirectToAction("GetAllTeam");
            }

            // If the model state is not valid, return to the edit view with the model data
            return View(model);
        }




    }
}

