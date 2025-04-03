using System.ComponentModel.DataAnnotations;

namespace Authentification.JWT.WebAPI.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
