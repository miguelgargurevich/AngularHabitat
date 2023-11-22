using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Repository.Contratos
{


    public interface IReciboConceptoRepository
    {
        Task<IEnumerable<ReciboConceptoReporte>> GetAll(ReciboConceptoReporte reciboConcepto);
        Task<bool> Update(ReciboConcepto reciboConcepto);
        Task<int> Add(ReciboConcepto reciboConcepto);
        Task<int> AddList(List<ReciboConcepto> entidadLista);
    }


}
