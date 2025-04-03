using Authentification.JWT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification.JWT.Service.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
         Task<User> RegisterUserAsync(string username, string email, string password);

        bool VerifyPassword(User user, string password);

        Task<User> GetUserById(int userId);

    }
}


