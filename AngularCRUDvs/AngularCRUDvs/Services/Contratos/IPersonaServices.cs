using AngularCRUDvs.Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCRUDvs.Services.Contratos
{
    public interface IPersonaServices
    {
        Task<Persona> GetPersona(int userId);
        Task<int> AddAsync(Persona persona);
        Task<bool> UpdateAsync(Persona persona);
        Task<bool> DeleteAsync(int personaId);
        Task<User> ValidarUsuario(User login);
        Task<User> ChangePassword(User user);

        Task<UserRole> PostUserRolesAddAsync(UserRole aspNetUserRole);
        Task<UserRole> GetUserRole(int UserId);
        Task<UserRole> PostUserRolesDelAsync(UserRole aspNetUserRole);

        Task<Role> GetRole(int roleId);
    }

}
