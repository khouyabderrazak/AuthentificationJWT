using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification.JWT.DAL.Models
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }


        [Required]
        public string PasswordHash { get; set; }
    }
}
