using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCRUDvs.Models
{
    public class PersonaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<DireccionModel> Direcciones { get; set; }

        
    }
}
