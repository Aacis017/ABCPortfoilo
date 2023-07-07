using ABCPortfolio.Data;
using ABCPortfolio.Models;
using ABCPortfolio.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ABCPortfolio.Controllers
{
    public class AdminController : Controller
    {
        
        private readonly AppDBContext _AppDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminController(AppDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _AppDBContext = appDBContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        { 
            return View();
        }
        //  public async Task<IActionResult> Logout()
        //{

        //await _signInManager.SignOutAsync();

        //    return RedirectToAction("Login");
        //  }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
             return RedirectToAction("Index", "Home");


        }
        public IActionResult Test()
        {
            return View();
        }
        //public async Task<IActionResult> LoginUsers(LoginViewModel loginViewModel)
        //{
        //    if(loginViewModel == null)
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    if((loginViewModel.UserName != "admin") || (loginViewModel.Password != "admin1")) {

        //        return RedirectToAction("Login");
        //    }
        //    // Login Cookies
        //    var claims = new[]{
        //     new Claim(ClaimTypes.Name, loginViewModel.UserName.ToString())

        //    };
        //    var claimsIdentity = new ClaimsIdentity(claims, AuthConfigModel.CookieScheme);
        //    await _httpContextAccessor.HttpContext!.SignInAsync(
        //        AuthConfigModel.CookieScheme, new ClaimsPrincipal(claimsIdentity)
        //        );
        //     return RedirectToAction("Index","Home");

        //}
        public async Task<IActionResult> LoginUsers(LoginViewModel loginViewModel)
        {
            if (loginViewModel == null)
            {
                return RedirectToAction("Login");
            }

            if (loginViewModel.UserName != "admin" || loginViewModel.Password != "admin1")
            {
                
                ModelState.AddModelError(string.Empty, "Wrong username or password");
                return View("Login", new Admin());
            }
            

            // Login Cookies
            var claims = new[]{
        new Claim(ClaimTypes.Name, loginViewModel.UserName.ToString())
    };
            
            var claimsIdentity = new ClaimsIdentity(claims, AuthConfigModel.CookieScheme);
            await _httpContextAccessor.HttpContext!.SignInAsync(
                AuthConfigModel.CookieScheme, new ClaimsPrincipal(claimsIdentity)
            );

            return RedirectToAction("Index", "AdminLayout");
        }


    }
}
