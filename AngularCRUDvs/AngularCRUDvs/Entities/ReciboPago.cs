using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AngularCRUDvs.Entidades
{
    public class ReciboPago
    {
        public int ReciboPagoId { get; set; }
        public int ReciboId { get; set; }
        public int UnidadId { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? MontoPago { get; set; }
        public string? urlVoucher { get; set; }
        public string? nombreVoucher { get; set; }

    }

}

