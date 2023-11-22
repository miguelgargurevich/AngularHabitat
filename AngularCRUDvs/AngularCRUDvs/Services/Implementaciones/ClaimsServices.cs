using AngularCRUDvs.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using AngularCRUDvs.Services.Contratos;
//using AngularCRUDvs.Repository.Contratos;
using System.Collections;
using AngularCRUDvs.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace AngularCRUDvs.Services.Implementaciones
{
    public class ClaimsServices : IClaimsServices
    {

        private readonly IConfiguration _config;

        public ClaimsServices(IConfiguration config)
        {
            _config = config;
        }

        public ClaimsIdentity addClaims(UsersModel user)
        {
            //add to claim
            List<Claim> c = new List<Claim>()
                                {

                                    new Claim(ClaimTypes.Name, user.UserName),
                                    new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                                    new Claim(ClaimTypes.Role, user.Role),
                                    new Claim("UnidadId", user.UnidadId.ToString()),
                                };
            ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);

            return ci;

        }

        public UsersModel getUserClaimsHttpContext(HttpContext httpContext)
        {
            var claims = httpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
            var obj = new UsersModel();
            obj.UserName = claims[0].Value;
            obj.UserId = (int)Convert.ToUInt32(claims[1].Value);
            obj.Role = claims[2].Value;
            obj.UnidadId = (int)Convert.ToUInt32(claims[3].Value);

            return obj;


        }


    }
}
