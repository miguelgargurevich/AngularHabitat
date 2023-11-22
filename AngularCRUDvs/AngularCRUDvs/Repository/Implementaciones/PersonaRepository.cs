
using System.Data.Common;
using System.Data;
//using AngularCRUDvs.Models;
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
using iText.Commons.Actions.Contexts;

namespace AngularCRUDvs.Repository.Implementaciones
{



    public class PersonaRepository : IPersonaRepository
    {
        //private readonly ApplicationDbContextFactory _context;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PersonaRepository> _logger;
        private readonly IMapper _mapper;

        public PersonaRepository(ApplicationDbContext context,
            ILogger<PersonaRepository> logger,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<int> AddAsync(Persona persona)
        {
            throw new NotImplementedException();
        }

        public Task<User> ChangePassword(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int personaId)
        {
            throw new NotImplementedException();
        }

        public async Task<Persona> GetPersona(int unidadId)
        {
            var query = _context.Persona.AsQueryable(); // Empiezas con la consulta completa

            if (unidadId != 0)
                query = query.Where(x => x.UnidadId == unidadId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<UserRole> GetUserRole(int userId)
        {
            UserRole obj = new UserRole();
            try
            {

                obj = await _context.UserRole
                            .Where(x => x.UserId == userId)
                            .Join(_context.Role, r => r.RoleId, s => s.RoleId, (r, s) => new UserRole { UserRoleId = r.UserRoleId, UserId = r.UserId, RoleId = s.RoleId })
                            .FirstAsync();

            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }

            return obj;
        }

        public async Task<Role> GetRole(int roleId)
        {
            Role obj = new Role();
            try
            {

                obj = await _context.Role
                            .Where(x => x.RoleId == roleId)
                            .FirstAsync();

            }
            catch (Exception ex)
            {
                CapturarError(ex);
            }

            return obj;
        }

        public Task<UserRole> PostUserRolesAddAsync(UserRole aspNetUserRole)
        {
            throw new NotImplementedException();
        }

        public Task<UserRole> PostUserRolesDelAsync(UserRole aspNetUserRole)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Persona persona)
        {
            throw new NotImplementedException();
        }

        public async Task<User> ValidarUsuario(User login)
        {
            var usuario = await _context.User.Where(x => x.UserName.Equals(login.UserName.ToUpper()))
                                            .Where(x => x.PasswordHash.Equals(login.PasswordHash))
                                            .FirstOrDefaultAsync();

            //UsersModel userModel = _mapper.Map<UsersModel>(usuario); //map ok just missing register in mappingProfile class

            //IQueryable<User> queryResult = from r in _context.User
            //                               where r.UserName.Equals(login.UserName)
            //                               && r.PasswordHash.Equals(login.PasswordHash)
            //                               && r.Estado == true
            //                                select new User
            //                                {
            //                                    UserId = r.UserId,
            //                                    UserName = r.UserName,
            //                                    Estado = r.Estado
            //                                };
            //var result = queryResult.ToList().FirstOrDefault();

            return usuario;


            //return usuario;

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

