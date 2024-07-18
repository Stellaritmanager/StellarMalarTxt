using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StellarBillingSystem.Models;

namespace StellarBillingSystem.Controllers;

[Authorize]
public class HomeController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }

  
    public IActionResult Privacy()
    {
        return View();
    }

}
    
