using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AngularCRUDvs.Entidades
{
    public class ReciboConcepto
    {
        public int ReciboConceptoId { get; set; }
        public int ReciboId { get; set; }
        public int ConceptoId { get; set; }
        public int UnidadId { get; set; }
        public decimal Total { get; set; }

    }

}

