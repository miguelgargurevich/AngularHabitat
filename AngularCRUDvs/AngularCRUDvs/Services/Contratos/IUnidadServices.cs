using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Services.Contratos
{


    public interface IUnidadServices
    {
        Task<Unidad> Get(Unidad unidad);
        Task<IEnumerable<Unidad>> GetAll();
        Task<int> Add(Unidad Unidad);
        Task<bool> Update(Unidad Unidad);
    }


}
