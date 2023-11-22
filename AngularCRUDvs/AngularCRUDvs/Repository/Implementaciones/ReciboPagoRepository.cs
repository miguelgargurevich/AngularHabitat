
using System.Data.Common;
using System.Data;
using AngularCRUDvs.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.Collections;
using AngularCRUDvs.Entidades;
using Microsoft.Data.SqlClient;
using AngularCRUDvs.Repository.Contratos;
using AngularCRUDvs.Services.Implementaciones;
using AngularCRUDvs.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AngularCRUDvs.Repository.Implementaciones
{



    public class ReciboPagoRepository : IReciboPagoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReciboPagoRepository> _logger;
        private readonly IMapper _mapper;

        public ReciboPagoRepository(ApplicationDbContext context,
            ILogger<ReciboPagoRepository> logger,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ReciboPago> Get(ReciboPago entidad)
        {
            var entidadRet = new ReciboPago();

            // Obtiene la entidad desde la base de datos
            entidadRet = await _context.ReciboPago
                .Where(x => x.ReciboId == entidad.ReciboId)
                .Where(x => x.UnidadId == entidad.UnidadId)
                .FirstOrDefaultAsync();

            if (entidadRet != null)
            {
                // Recarga la entidad desde la base de datos
                _context.Entry(entidadRet).Reload();
            }

            // Si no se encuentra la entidad, puedes retornar null o manejarlo de otra manera según tus necesidades.
            return entidadRet;
        }


        public async Task<IEnumerable<ReciboPago>> GetAll()
        {
            return await _context.ReciboPago.ToListAsync();
        }

        public async Task<bool> Update(ReciboPago ReciboPago)
        {
            try
            {
                _context.ReciboPago.Update(ReciboPago);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                CapturarError(ex);
                return false;
            }
        }

        public async Task<int> Add(ReciboPago entidad)
        {
            int ReciboPagoId = 0;

            try
            {

                var existingEntity = _context.ReciboPago
                    .SingleOrDefault(r => r.ReciboId == entidad.ReciboId && r.UnidadId == entidad.UnidadId);

                if (existingEntity != null)
                    _context.ReciboPago.Remove(existingEntity);

                _context.ReciboPago.Add(entidad);
                await _context.SaveChangesAsync();
                ReciboPagoId = entidad.ReciboPagoId;

            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }

            return ReciboPagoId;
        }


        #region "Control de errores"

        public void CapturarError(Exception error, string controlador = "", string accion = "")
        {
            var msg = error.Message;
            if (error.InnerException != null)
            {
                msg = msg + "/;/" + error.InnerException.Message;
                if (error.InnerException.InnerException != null)
                {
                    msg = msg + "/;/" + error.InnerException.InnerException.Message;
                    if (error.InnerException.InnerException.InnerException != null)
                        msg = msg + "/;/" + error.InnerException.InnerException.InnerException.Message;
                }
            }

            var fechahora = DateTime.Now.ToString();
            var comentario = $@"***ERROR: [{fechahora}] [{controlador}/{accion}] - MensajeError: {msg}";
            string errorFormat = string.Format("{0} | {1}", comentario, error.StackTrace);
            _logger.LogError(errorFormat);

        }



        #endregion


    }


}
