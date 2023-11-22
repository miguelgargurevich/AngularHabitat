using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using AngularCRUDvs.Models;
using AngularCRUDvs.Services.Contratos;
using AngularCRUDvs.Services.Implementaciones;

namespace AngularCRUDvs.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    IClaimsServices _claimsServices;

    public HomeController(ILogger<HomeController> logger, IClaimsServices claimsServices)
    {
        _logger = logger;
        _claimsServices = claimsServices;
    }

    public IActionResult Index()
    {
       var modelo = new UsersModel();

        string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
        if (userName != null)
        {
            //var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            UsersModel userLogin = _claimsServices.getUserClaimsHttpContext(HttpContext);

            ViewBag.Role = userLogin.Role;
            ViewBag.IsSignedIn = true;

            //modelo.monthSelect = DateTime.Today.Month;
            //modelo.yearSelect = DateTime.Today.Year;

            modelo = userLogin;
        }
        else
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        return View(modelo);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Salir()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

