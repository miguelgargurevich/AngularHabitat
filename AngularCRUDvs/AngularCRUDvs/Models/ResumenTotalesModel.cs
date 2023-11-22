using System;
using System.Globalization;

namespace AngularCRUDvs.Models
{
    public class ResumenTotalesModel
    {
        public decimal SumaAdministracion { get; set; }
        public string SumaAdministracionStr { get => SumaAdministracion.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaLuzComunCondominio { get; set; }
        public string SumaLuzComunCondominioStr { get => SumaLuzComunCondominio.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaLuzComunBlock { get; set; }
        public string SumaLuzComunBlockStr { get => SumaLuzComunBlock.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaAguacomunCondominio { get; set; }
        public string SumaAguacomunCondominioStr { get => SumaAguacomunCondominio.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaAguadpto { get; set; }
        public string SumaAguadptoStr { get => SumaAguadpto.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaContingenciaBlock { get; set; }
        public string SumaContingenciaBlockStr { get => SumaContingenciaBlock.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaContingenciaCondominio { get; set; }
        public string SumaContingenciaCondominioStr { get => SumaContingenciaCondominio.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaMantenimientoAsensor { get; set; }
        public string SumaMantenimientoAsensorStr { get => SumaMantenimientoAsensor.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaMantenimientoCisterna { get; set; }
        public string SumaMantenimientoCisternaStr { get => SumaMantenimientoCisterna.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaMulta { get; set; }
        public string SumaMultaStr { get => SumaMulta.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaSubtotal { get; set; }
        public string SumaSubtotalStr { get => SumaSubtotal.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaDeudaAnterior { get; set; }
        public string SumaDeudaAnteriorStr { get => SumaDeudaAnterior.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaTotal { get; set; }
        public string SumaTotalStr { get => SumaTotal.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaMultaNoPago { get; set; }
        public string SumaMultaNoPagoStr { get => SumaMultaNoPago.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaMontoPago { get; set; }
        public string SumaMontoPagoStr { get => SumaMontoPago.ToString("0.00", new CultureInfo("en-US")); }

        public decimal SumaCargoSiguienteMes { get; set; }
        public string SumaCargoSiguienteMesStr { get => SumaCargoSiguienteMes.ToString("0.00", new CultureInfo("en-US")); }
    }
}

