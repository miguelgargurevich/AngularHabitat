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
using AngularCRUDvs.Repository.Implementaciones;

namespace AngularCRUDvs.Services.Implementaciones
{
    public class PersonaServices : IPersonaServices
    {

        private readonly IConfiguration _config;
        private readonly IPersonaRepository _personaRepository;
        

        public PersonaServices(IConfiguration config,
            IPersonaRepository personaRepository)
        {
            _config = config;
            _personaRepository = personaRepository;
        }


        public async Task<Persona> GetPersona(int userId)
        {
            return await _personaRepository.GetPersona(userId);
        }

        public async Task<int> AddAsync(Persona persona)
        {
            return await _personaRepository.AddAsync(persona);
        }

        public async Task<bool> UpdateAsync(Persona persona)
        {
            return await _personaRepository.UpdateAsync(persona);
        }

        public async Task<bool> DeleteAsync(int personaId)
        {
            return await _personaRepository.DeleteAsync(personaId);
        }

        public async Task<User> ValidarUsuario(User login)
        {
            return await _personaRepository.ValidarUsuario(login);
        }

        public async Task<User> ChangePassword(User user)
        {
            return await _personaRepository.ChangePassword(user);

        }

        public async Task<UserRole> PostUserRolesDelAsync(UserRole UserRole)
        {

            return await _personaRepository.PostUserRolesDelAsync(UserRole);
             
        }

        public async Task<UserRole> GetUserRole(int UserId)
        {
            return await _personaRepository.GetUserRole(UserId);

        }

        public async Task<UserRole> PostUserRolesAddAsync(UserRole UserRoles)
        {
            return await _personaRepository.PostUserRolesAddAsync(UserRoles);
        }

        public async Task<Role> GetRole(int roleId) {
            return await _personaRepository.GetRole(roleId);
        }
    }
}
