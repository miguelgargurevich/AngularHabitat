using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AngularCRUDvs.Entidades
{
    public class Concepto
    {
        public int ConceptoId { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }
        public bool EsCalculado { get; set; }
    }

}


//Administracion
//LuzComunCondominio
//LuzComunBlock 
//AguacomunCondominio 
//Aguadpto
//ContingenciaBlock
//ContingenciaCondominio 
//MantenimientoAsensor 
//MantenimientoCisterna 
//MultaActual  
//DeudaAnterior 

//MultaSiguienteMes
//CargoSiguienteMes
