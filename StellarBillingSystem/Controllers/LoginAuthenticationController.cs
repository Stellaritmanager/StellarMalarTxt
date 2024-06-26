using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StellarBillingSystem.Context;
using System.Security.Claims;
using StellarBillingSystem.Models;

namespace StellarBillingSystem.Controllers
{
    public class LoginAuthenticationController : Controller
    {
        private BillingContext _billingContext;

        public LoginAuthenticationController(BillingContext billingContext)
        {
            _billingContext = billingContext;
        }


        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }


        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "LoginAuthentication");
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignUpModel model)
        {
            var login = await _billingContext.SHSignUp.FindAsync(model.Username);

            if (login != null)
            {
                if (login.Password == model.Password)
                {
                    login.Username = model.Username;

                    login.Password = model.Password;

                    List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Username),
                    new Claim("OtherProperties", "Example Role")
                };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme
                );
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);



                    return RedirectToAction("Index", "Home");
                }

            }
            ViewBag.Message = " Username Not Found";

            return View();

        }
    }
}
