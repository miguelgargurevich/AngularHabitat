using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Repository.Contratos
{
    

    public interface IConceptoRepository
    {
        Task<bool> Add(Concepto entidad);
        Task<List<Concepto>> AddList(List<Concepto> listEntidad);
    }


}
