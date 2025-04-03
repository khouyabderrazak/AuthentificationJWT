using System.ComponentModel.DataAnnotations;

namespace Authentification.JWT.WebAPI.Models
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
