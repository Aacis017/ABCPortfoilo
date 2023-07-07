using ABCPortfolio.Data;
using ABCPortfolio.Models;
using ABCPortfolio.Services;
using ABCPortfolio.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static ABCPortfolio.Helper.ToasterHelper;

namespace ABCPortfolio.Controllers
{
    public class ContactController : Controller
    {
        

        private readonly IContactService _service;

        public ContactController(AppDBContext context)
        {

            _service = new ContactService(context);
        }
        public IActionResult Submit()
        {
           

            ViewBag.ServiceType = new List<SelectListItem>() {
                new SelectListItem("Service Type 1", "0"),
                new SelectListItem("Service Type 2", "1")
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(ContactUsViewModel contactUsViewModel)
        {
            if (ModelState.IsValid)
            {
                TempData.AddToastMessage("Success", "Toaster notification test!", ToastType.Success);
                var result= _service.Contact(contactUsViewModel);
                return RedirectToAction("Submit");
                
            }
            else
            {

            }

            ViewBag.ServiceType = new List<SelectListItem>() {
                new SelectListItem("Service Type 1", "0"),
                new SelectListItem("Service Type 2", "1")
            };
            return View(contactUsViewModel);
        }
        //private IEnumerable<SelectListItem> EnumtoSelectListItem(Type? t)
        //{
        //    return Enum.GetValues(typeof(t)).Cast<t>().Select(v => new SelectListItem
        //    {
        //        Text = v.ToString(),
        //        Value = ((int)v).ToString()
        //    }).ToList();
        //}

        [Authorize]
        public IActionResult Index()
        {
            var contacts = _service.GetContact();
            return View(contacts);
        }
    }
    
}
