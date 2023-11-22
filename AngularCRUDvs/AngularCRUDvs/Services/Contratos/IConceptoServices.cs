using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Services.Contratos
{
    
    public interface IConceptoServices
    {
        Task<bool> Add(Concepto entidad);
        Task<List<Concepto>> AddList(List<Concepto> listEntidad);
    }


}
