using ABCPortfolio.Data;
using ABCPortfolio.Models;
using ABCPortfolio.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ABCPortfolio.Controllers
{
    public class FaqController : Controller
    {
        private readonly AppDBContext _AppDBContext;
        public FaqController(AppDBContext AppDBContext)
        {
            _AppDBContext = AppDBContext;

        }
        [Authorize]
        public IActionResult Faq()
        {
            return View();
        }
      //  [HttpPost]
      //  [Authorize]
        // public IActionResult AddFaqDetails(Faq faq)
        // {
        //     //save the data
        ////     _AppDBContext.Faqs.Add(faq);

        //         _AppDBContext.Faqs.Add(faq);

        //      _AppDBContext.SaveChanges();
        //     return RedirectToAction("Faq");

        // }
        // public IActionResult GetAllFaq()
        // {
        //     var faqs = _AppDBContext.Faqs.ToList();
        //     return View(faqs);
        // }
        // public IActionResult Index()
        // {
        //     return View();
        // }

        // public IActionResult GetFaqById(int id) 
        // { 
        //     Faq faq =_AppDBContext.Faqs.FirstOrDefault(x => x.Id == id);    
        //     return View(faq);   

        // }
        // public IActionResult UpdateFaqById(Faq faq
        //     )
        // {

        //     _AppDBContext.Faqs.Update(faq);
        //     _AppDBContext.SaveChanges();
        //     return RedirectToAction("GetAllFaq");

        // }
        // public IActionResult RemoveFaqById(Faq faq)

        // {

        //     _AppDBContext.Faqs.Remove(faq);
        //     _AppDBContext.SaveChanges();
        //     return RedirectToAction("GetAllFaq");

        // }
        // }
    }
}
