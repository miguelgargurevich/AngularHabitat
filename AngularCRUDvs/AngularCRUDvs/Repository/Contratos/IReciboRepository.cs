using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Repository.Contratos
{


    public interface IReciboRepository
    {
        Task<Recibo> Get(Recibo recibo);
        Task<IEnumerable<Recibo>> GetAll(Recibo recibo);
        Task<bool> Update(Recibo recibo);
        Task<int> Add(Recibo entidad);
    }


}
