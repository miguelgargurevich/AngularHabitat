
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



    public class UnidadRepository : IUnidadRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnidadRepository> _logger;
        private readonly IMapper _mapper;

        public UnidadRepository(ApplicationDbContext context,
            ILogger<UnidadRepository> logger,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unidad> Get(Unidad unidad)
        {
            var query = _context.Unidad.AsQueryable(); // Empiezas con la consulta completa

            if (unidad.UnidadId != 0)
                query = query.Where(x => x.UnidadId == unidad.UnidadId);
            if (unidad.UserId != 0)
                query = query.Where(x => x.UserId == unidad.UserId);
            if (!string.IsNullOrEmpty(unidad.Dpto))
                query = query.Where(x => x.Dpto == unidad.Dpto);

            if (!string.IsNullOrEmpty(unidad.Block))
                query = query.Where(x => x.Block == unidad.Block);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Unidad>> GetAll()
        {
            return await _context.Unidad.ToListAsync();
        }

        public async Task<bool> Update(Unidad Unidad)
        {
            try
            {
                _context.Unidad.Update(Unidad);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                CapturarError(ex);
                return false;
            }
        }

        public async Task<int> Add(Unidad entidad)
        {
            int UnidadId = 0; 

            try
            {
                var existingUnidad = _context.Unidad
                    .SingleOrDefault(r => r.Block == entidad.Block && r.Dpto == entidad.Dpto);

                if (existingUnidad == null)
                {
                    _context.Unidad.Add(entidad);
                    await _context.SaveChangesAsync();
                    UnidadId = entidad.UnidadId;
                }
                else
                {
                    UnidadId = existingUnidad.UnidadId;
                }
            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }

            return UnidadId; // Devolvemos el ID del Unidad creado o actualizado
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
