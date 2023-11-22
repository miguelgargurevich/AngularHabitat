using System.ComponentModel.DataAnnotations.Schema;

namespace AngularCRUDvs.Models
{
    public class UsersModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string?  PasswordHash { get; set; }
        public bool? Estado { get; set; }
        public string? Role { get; set; }
        public int UnidadId { get; set; }
        
        [NotMapped]
        public bool MantenerActivo { get; set; }

    }
}