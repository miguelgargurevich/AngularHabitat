using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AngularCRUDvs.Models
{
    public class ReciboConceptoReporteModel 
    {
        public int ReciboConceptoId { get; set; }
        public int ReciboId { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public int ConceptoId { get; set; }
        public int UnidadId { get; set; }
        public decimal Total { get; set; }


        public string? Block { get; set; }
        public string? Dpto { get; set; }

        public string? DescripcionConcepto { get; set; }
        public string? DescripcionRecibo { get; set; }

    }

}

