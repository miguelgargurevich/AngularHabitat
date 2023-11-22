using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AngularCRUDvs.Entidades
{
    public class Persona
    {
        public int PersonaId { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NroDocumento { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Email { get; set; } 
        public string? Telefono { get; set; }
        //public bool ViveEnUnidad { get; set; }
        public int UnidadId { get; set; }

    }

}

