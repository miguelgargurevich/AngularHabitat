using System.Collections.Generic;
using System.Threading.Tasks;
using AngularCRUDvs.Entidades;

namespace AngularCRUDvs.Repository.Contratos
{
    

    public interface IPersonaRepository
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
