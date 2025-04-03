using System.ComponentModel.DataAnnotations;

namespace Authentification.JWT.Service.DTOS
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
