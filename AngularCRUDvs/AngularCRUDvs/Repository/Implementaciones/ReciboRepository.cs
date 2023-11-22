using System.Data;
using AngularCRUDvs.Entidades;
using AngularCRUDvs.Repository.Contratos;
using AngularCRUDvs.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AngularCRUDvs.Repository.Implementaciones
{

    public class ReciboRepository : IReciboRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReciboRepository> _logger;
        private readonly IMapper _mapper;

        public ReciboRepository(ApplicationDbContext context,
            ILogger<ReciboRepository> logger,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Recibo> Get(Recibo recibo)
        {
            var query = _context.Recibo.AsQueryable(); // Empiezas con la consulta completa

            if (recibo.ReciboId != 0)
                query = query.Where(x => x.ReciboId == recibo.ReciboId);
            
            if (recibo.Mes != 0)
                query = query.Where(x => x.Mes == recibo.Mes);

            if (recibo.Anio != 0)
                query = query.Where(x => x.Anio == recibo.Anio);

            return await query.FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Recibo>> GetAll(Recibo recibo)
        {
            var query = _context.Recibo.AsQueryable(); // Empiezas con la consulta completa
            List<Recibo> list = new List<Recibo>();
            try
            {
               
                if (recibo.Mes != 0)
                    query = query.Where(x => x.Mes == recibo.Mes);

                if (recibo.Anio != 0)
                    query = query.Where(x => x.Anio == recibo.Anio);

                list = await query.ToListAsync();
            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }
            return list.ToList();

        }

        public async Task<bool> Update(Recibo recibo)
        {
            try
            {
                _context.Recibo.Update(recibo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                CapturarError(ex);
                return false;
            }
        }

        public async Task<int> Add(Recibo entidad)
        {
            int reciboId = 0; 

            try
            {
                var existingRecibo = _context.Recibo
                    .SingleOrDefault(r => r.Mes == entidad.Mes && r.Anio == entidad.Anio);

                if (existingRecibo == null)
                {
                    _context.Recibo.Add(entidad);
                    await _context.SaveChangesAsync();
                    reciboId = entidad.ReciboId;
                }
                else
                {
                    reciboId = existingRecibo.ReciboId;
                }
            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }

            return reciboId; // Devolvemos el ID del recibo creado o actualizado
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
