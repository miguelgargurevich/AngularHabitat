using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Repository.Contratos
{


    public interface IReciboPagoRepository
    {
        Task<ReciboPago> Get(ReciboPago entidad);
        Task<IEnumerable<ReciboPago>> GetAll();
        Task<bool> Update(ReciboPago ReciboPago);
        Task<int> Add(ReciboPago entidad);
    }


}
