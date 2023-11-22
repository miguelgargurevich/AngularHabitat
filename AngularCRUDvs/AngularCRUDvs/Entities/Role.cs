using System;
using Microsoft.AspNetCore.Identity;

namespace AngularCRUDvs.Entidades
{
    public class Role
    {
        public int RoleId { get; set; }
        public string? Name { get; set; }
        public string? Estado { get; set; }
    }
}

