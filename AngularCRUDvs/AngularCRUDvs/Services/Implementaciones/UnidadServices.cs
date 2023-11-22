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
    public class UnidadServices : IUnidadServices
    {

        private readonly IConfiguration _config;
        private readonly IUnidadRepository _UnidadRepository;


        public UnidadServices(IConfiguration config,
            IUnidadRepository UnidadRepository)
        {
            _config = config;
            _UnidadRepository = UnidadRepository;
        }

        public async Task<Unidad> Get(Unidad unidad)
        {
            return await _UnidadRepository.Get(unidad);
        }

        public async Task<IEnumerable<Unidad>> GetAll()
        {
            return await _UnidadRepository.GetAll();
        }

        public async Task<int> Add(Unidad Unidad)
        {
            return await _UnidadRepository.Add(Unidad);
        }

        public async Task<bool> Update(Unidad Unidad)
        {
            return await _UnidadRepository.Update(Unidad);
        }


    }
}
