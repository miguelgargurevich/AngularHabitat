
using System.Data;
using AngularCRUDvs.Entidades;
using AngularCRUDvs.Repository.Contratos;
using AngularCRUDvs.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AngularCRUDvs.Repository.Implementaciones
{
    public class ReciboConceptoRepository : IReciboConceptoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReciboConceptoRepository> _logger;
        private readonly IMapper _mapper;

        public ReciboConceptoRepository(ApplicationDbContext context,
            ILogger<ReciboConceptoRepository> logger,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ReciboConceptoReporte>> GetAll(ReciboConceptoReporte reciboConcepto)
        {
            var list = new List<ReciboConceptoReporte>();
            try
            {
                // Empiezas con la consulta completa
                var query = _context.ReciboConcepto.AsQueryable()
                                .Join(_context.Recibo, rc => rc.ReciboId, r => r.ReciboId, (rc, r) => new { rc, r })
                                .Join(_context.Concepto, j => j.rc.ConceptoId, c => c.ConceptoId, (j, c) => new { j.rc, j.r, c })
                                .Join(_context.Unidad, j => j.rc.UnidadId, u => u.UnidadId, (j, u) => new { j.rc, j.r, j.c, u });
                
                if (reciboConcepto.ReciboId != 0)
                    query = query.Where(x => x.r.ReciboId == reciboConcepto.ReciboId);

                if (reciboConcepto.UnidadId != 0)
                    query = query.Where(x => x.rc.UnidadId == reciboConcepto.UnidadId);

                if (reciboConcepto.Anio != 0)
                    query = query.Where(x => x.r.Anio == reciboConcepto.Anio);

                list = await query.Select(j => new ReciboConceptoReporte
                                {
                                    ReciboId = j.r.ReciboId,
                                    ConceptoId = j.c.ConceptoId,
                                    UnidadId = j.u.UnidadId,
                                    ReciboConceptoId = j.rc.ReciboConceptoId,
                                    Total = j.rc.Total,
                                    DescripcionConcepto = j.c.Descripcion,
                                    Block = j.u.Block,
                                    Dpto = j.u.Dpto,
                                    Mes = Convert.ToInt32(j.r.Mes),
                                    Anio = Convert.ToInt32(j.r.Anio),
                                    DescripcionRecibo = j.r.Descripcion,
                                }).ToListAsync();

            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }
          

            return list;
        }

        public async Task<IEnumerable<ReciboConceptoReporte>> GetAllQueryrable(ReciboConcepto reciboConcepto)
        {
            var list = new List<ReciboConceptoReporte>();
            try
            {

                IQueryable<ReciboConceptoReporte> queryResult = from rc in _context.ReciboConcepto
                                                                join r in _context.Recibo on rc.ReciboId equals r.ReciboId
                                                                join c in _context.Concepto on rc.ConceptoId equals c.ConceptoId
                                                                join u in _context.Unidad on rc.UnidadId equals u.UnidadId
                                                                where rc.ReciboId == reciboConcepto.ReciboId
                                                                select new ReciboConceptoReporte
                                                                {
                                                                    ReciboId = r.ReciboId,
                                                                    ConceptoId = c.ConceptoId,
                                                                    UnidadId = u.UnidadId,
                                                                    ReciboConceptoId = rc.ReciboConceptoId,
                                                                    Total = rc.Total,
                                                                    DescripcionConcepto = c.Descripcion,
                                                                    Block = u.Block,
                                                                    Dpto = u.Dpto
                                                                };
                list = await queryResult.ToListAsync();
            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }


            return list;
        }

        public async Task<bool> Update(ReciboConcepto recibo)
        {
            try
            {
                _context.ReciboConcepto.Update(recibo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                CapturarError(ex);
                return false;
            }
        }

        public async Task<int> Add(ReciboConcepto entidad)
        {
            int reciboId = 0; 

            try
            {
                var existingReciboConceptos = _context.ReciboConcepto
                    .SingleOrDefault(r => r.ReciboId == entidad.ReciboId);

                if (existingReciboConceptos != null)
                {
                    _context.ReciboConcepto.RemoveRange(existingReciboConceptos);
                }
                _context.ReciboConcepto.Add(entidad);
                await _context.SaveChangesAsync();
                reciboId = entidad.ReciboId;
            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }

            return reciboId; 
        }

        public async Task<int> AddList(List<ReciboConcepto> entidadLista)
        {
            int reciboId = entidadLista.First().ReciboId;

            try
            {
                var existingReciboConceptos = _context.ReciboConcepto
                    .Where(r => r.ReciboId == reciboId);

                if (existingReciboConceptos != null)
                {
                    _context.ReciboConcepto.RemoveRange(existingReciboConceptos);
                }

                _context.ReciboConcepto.AddRange(entidadLista);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }

            return reciboId;
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
