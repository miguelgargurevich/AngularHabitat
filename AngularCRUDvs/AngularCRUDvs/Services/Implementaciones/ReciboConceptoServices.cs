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
    public class ReciboConceptoServices : IReciboConceptoServices
    {

        private readonly IConfiguration _config;
        private readonly IReciboConceptoRepository _ReciboConceptoRepository;


        public ReciboConceptoServices(IConfiguration config,
            IReciboConceptoRepository ReciboConceptoRepository)
        {
            _config = config;
            _ReciboConceptoRepository = ReciboConceptoRepository;
        }

        public async Task<IEnumerable<ReciboConceptoReporte>> GetAll(ReciboConceptoReporte reciboConcepto)
        {
            return await _ReciboConceptoRepository.GetAll(reciboConcepto);
        }

        public async Task<int> Add(ReciboConcepto reciboConcepto)
        {
            return await _ReciboConceptoRepository.Add(reciboConcepto);
        }

        public async Task<bool> Update(ReciboConcepto reciboConcepto)
        {
            return await _ReciboConceptoRepository.Update(reciboConcepto);
        }

        public async Task<int> AddList(List<ReciboConcepto> entidadLista)
        {
            return await _ReciboConceptoRepository.AddList(entidadLista);
        }

    }
}
