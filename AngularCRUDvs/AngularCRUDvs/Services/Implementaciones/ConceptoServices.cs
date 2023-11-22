using AngularCRUDvs.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using AngularCRUDvs.Services.Contratos;
using AngularCRUDvs.Repository.Contratos;
using System.Collections;

namespace AngularCRUDvs.Services.Implementaciones
{
    public class ConceptoServices : IConceptoServices
    {

        private readonly IConfiguration _config;
        private readonly IConceptoRepository _ConceptoRepository;
        

        public ConceptoServices(IConfiguration config,
            IConceptoRepository ConceptoRepository)
        {
            _config = config;
            _ConceptoRepository = ConceptoRepository;
        }

        public async Task<bool> Add(Concepto entidad)
        {
            return await _ConceptoRepository.Add(entidad);
        }

        public async Task<List<Concepto>> AddList(List<Concepto> listEntidad)
        {
            return await _ConceptoRepository.AddList(listEntidad);
        }

      


    }
}
