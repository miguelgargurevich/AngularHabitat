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
    public class ReciboServices : IReciboServices
    {

        private readonly IConfiguration _config;
        private readonly IReciboRepository _ReciboRepository;


        public ReciboServices(IConfiguration config,
            IReciboRepository ReciboRepository)
        {
            _config = config;
            _ReciboRepository = ReciboRepository;
        }

        public async Task<Recibo> Get(Recibo recibo)
        {
            return await _ReciboRepository.Get(recibo);
        }

        public async Task<IEnumerable<Recibo>> GetAll(Recibo recibo)
        {
            return await _ReciboRepository.GetAll(recibo);
        }

        public async Task<int> Add(Recibo Recibo)
        {
            return await _ReciboRepository.Add(Recibo);
        }

        public async Task<bool> Update(Recibo Recibo)
        {
            return await _ReciboRepository.Update(Recibo);
        }


    }
}
