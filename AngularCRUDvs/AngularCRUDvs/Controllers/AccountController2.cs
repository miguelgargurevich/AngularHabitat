//using System.Data.SqlClient;
using System.Security.Claims;
using AngularCRUDvs.Data;
using AngularCRUDvs.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using AngularCRUDvs.Entidades;
using AutoMapper;
using AngularCRUDvs.Util;
using AngularCRUDvs.Services.Contratos;
using AngularCRUDvs.Services.Implementaciones;

namespace AngularCRUDvs.Controllers
{
    public class AccountController2 : Controller
    {
        Seguridad Seguridad = new Seguridad();
        private IConfiguration _config;
        private IMapper _mapper;
        private IPersonaServices _personaServices;
        private IClaimsServices _claimsServices;
        private IUnidadServices _unidadServices;

        public AccountController2(IConfiguration config, IMapper mapper, IPersonaServices personaServices, IClaimsServices claimsServices, IUnidadServices unidadServices)
        {
            _config = config;
            _mapper = mapper;
            _personaServices = personaServices;
            _claimsServices = claimsServices;
            _unidadServices = unidadServices;

        }
        public IActionResult Login()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                
                if (c.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsersModel userModel)
        {

            try
            {
                var passEncrypt = Seguridad.Encriptar(userModel.PasswordHash);
                //var passDesencrypt = Seguridad.DesEncriptar(u.Clave);

                var userFilter = new User() { UserName = userModel.UserName, PasswordHash = passEncrypt };
                User user = await _personaServices.ValidarUsuario(userFilter);
                UserRole userRole = await _personaServices.GetUserRole(user.UserId);
                Role role = await _personaServices.GetRole(userRole.RoleId);

                var unidadFilter = new Unidad() { UserId = user.UserId };
                Unidad unidad = await _unidadServices.Get(unidadFilter); //debe traer persona para traer el IdUnidad


                //userModel = _mapper.Map<UsersModel>(user);
                userModel.UserId = user.UserId;
                userModel.Role = role.Name;
                userModel.Estado = user.Estado;
                userModel.UserName = user.UserName;

                if (unidad != null)
                    userModel.UnidadId = unidad.UnidadId;

                ClaimsIdentity ci = _claimsServices.addClaims(userModel);
                AuthenticationProperties p = new();

                p.AllowRefresh = true;
                p.IsPersistent = userModel.MantenerActivo;


                if (!userModel.MantenerActivo)
                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60);
                else
                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);


                return RedirectToAction("Index", "Home");


            }
            catch (System.Exception e)
            {
                ViewBag.Error = "¡Error al ingresar al sistema! Los datos ingresados no son correctos, o puede que su usuario no esté activo."; // e.Message;
                return View();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}