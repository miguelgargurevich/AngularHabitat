using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Services.Contratos
{


    public interface IReciboPagoServices
    {
        Task<ReciboPago> Get(ReciboPago entidad);
        Task<IEnumerable<ReciboPago>> GetAll();
        Task<int> Add(ReciboPago ReciboPago);
        Task<bool> Update(ReciboPago ReciboPago);
    }


}
