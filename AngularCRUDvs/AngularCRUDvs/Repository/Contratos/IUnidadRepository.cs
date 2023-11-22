using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Repository.Contratos
{


    public interface IUnidadRepository
    {
        Task<Unidad> Get(Unidad unidad);
        Task<IEnumerable<Unidad>> GetAll();
        Task<bool> Update(Unidad Unidad);
        Task<int> Add(Unidad entidad);
    }


}
