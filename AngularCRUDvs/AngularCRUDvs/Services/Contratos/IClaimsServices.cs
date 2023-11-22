using AngularCRUDvs.Entidades;
using AngularCRUDvs.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AngularCRUDvs.Services.Contratos
{
    public interface IClaimsServices
    {
        ClaimsIdentity addClaims(UsersModel appUser);
        UsersModel getUserClaimsHttpContext(HttpContext httpContext);


    }

}
