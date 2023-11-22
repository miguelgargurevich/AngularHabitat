
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


    public class ConceptoRepository : IConceptoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ConceptoRepository> _logger;
        private readonly IMapper _mapper;

        public ConceptoRepository(ApplicationDbContext context,
            ILogger<ConceptoRepository> logger,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> Add(Concepto entidad)
        {
            try {
                _context.Concepto.Add(entidad);
               await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }

            return true;

        }

        public async Task<List<Concepto>> AddList(List<Concepto> listEntidad)
        {
            List<Concepto> resultList = new List<Concepto>();

            try
            {
                foreach (var entidad in listEntidad)
                {
                    // Verificar si el registro ya existe en la base de datos
                    var existingConcepto = await _context.Concepto
                        .FirstOrDefaultAsync(c => c.Descripcion == entidad.Descripcion);

                    if (existingConcepto == null)
                    {
                        // Agregar el registro solo si no existe
                        _context.Concepto.Add(entidad);

                        // Guardar los cambios para obtener el ID asignado
                        await _context.SaveChangesAsync();

                        // Agregar el registro a la lista resultante con el nuevo ID
                        resultList.Add(entidad);
                    }
                    else
                    {
                        // Agregar el registro existente a la lista resultante
                        resultList.Add(existingConcepto);
                    }
                }
            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }

            return resultList;
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
