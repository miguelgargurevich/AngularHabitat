using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Services.Contratos
{


    public interface IReciboServices
    {
        Task<Recibo> Get(Recibo recibo);
        Task<IEnumerable<Recibo>> GetAll(Recibo recibo);
        Task<int> Add(Recibo recibo);
        Task<bool> Update(Recibo recibo);
    }


}
