using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Services.Contratos
{


    public interface IReciboConceptoServices
    {
        Task<IEnumerable<ReciboConceptoReporte>> GetAll(ReciboConceptoReporte reciboConcepto);
        Task<int> Add(ReciboConcepto reciboConcepto);
        Task<bool> Update(ReciboConcepto reciboConcepto);
        Task<int> AddList(List<ReciboConcepto> entidadLista);
    }


}
