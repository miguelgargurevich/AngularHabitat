using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace AngularCRUDvs.Entidades
{
    public class Recibo
    {
        public int ReciboId { get; set; } //1
        public string? Descripcion { get; set; } //mantenimiento Agosto
        public DateTime FechaEmision { get; set; } //
        public DateTime FechaVencimiento { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public string? Estado { get; set; } //

        public string FechaEmisionStr => FechaEmision.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
        public string FechaVencimientoStr => FechaVencimiento.ToString("dd/MM/yyyy", new CultureInfo("en-US"));


    }

}

