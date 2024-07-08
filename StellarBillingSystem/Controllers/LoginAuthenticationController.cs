using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StellarBillingSystem.Context;
using System.Security.Claims;
using StellarBillingSystem.Models;
using Newtonsoft.Json;
using StellarBillingSystem.Business;

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


        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "LoginAuthentication");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        [HttpPost]
        public async Task<IActionResult> Login(StaffAdminModel model)
        {
            var login = await _billingContext.SHStaffAdmin.FindAsync(model.UserName);

            if (login != null)
            {
                if (login.Password == model.Password)
                {
                    login.UserName = model.UserName;

                    login.Password = model.Password;

                    List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, login.UserName),
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

                    BusinessClassBilling Busreg = new BusinessClassBilling(_billingContext);

                    var rolldetail = Busreg.GetRoll(model.UserName);



                    // Set TempData with the filtered roll details
                    TempData["RollAccess"] = JsonConvert.SerializeObject(rolldetail);




                    return RedirectToAction("Index","Home");
                }

            }
            ViewBag.Message = " Username Not Found";

            return View();

        }
    }
}
