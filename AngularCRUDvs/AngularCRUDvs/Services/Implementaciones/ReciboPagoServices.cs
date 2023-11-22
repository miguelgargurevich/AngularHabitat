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
    public class ReciboPagoServices : IReciboPagoServices
    {

        private readonly IConfiguration _config;
        private readonly IReciboPagoRepository _ReciboPagoRepository;


        public ReciboPagoServices(IConfiguration config,
            IReciboPagoRepository ReciboPagoRepository)
        {
            _config = config;
            _ReciboPagoRepository = ReciboPagoRepository;
        }

        public async Task<ReciboPago> Get(ReciboPago entidad)
        {
            return await _ReciboPagoRepository.Get(entidad);
        }

        public async Task<IEnumerable<ReciboPago>> GetAll()
        {
            return await _ReciboPagoRepository.GetAll();
        }

        public async Task<int> Add(ReciboPago ReciboPago)
        {
            return await _ReciboPagoRepository.Add(ReciboPago);
        }

        public async Task<bool> Update(ReciboPago ReciboPago)
        {
            return await _ReciboPagoRepository.Update(ReciboPago);
        }


    }
}
