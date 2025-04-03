using System.ComponentModel.DataAnnotations;

namespace Authentification.JWT.Service.DTOS
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
