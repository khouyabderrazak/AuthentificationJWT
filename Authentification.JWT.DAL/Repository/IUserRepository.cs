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
        Task<UserDto> GetUserByUsernameAsync(string username);
         Task<UserDto> RegisterUserAsync(string username, string email, string password);

        bool VerifyPassword(UserDto user, string password);

        Task<int> GetIdUser(UserDto user);

        Task<User> GetUserById(int userId);
        //Task<UserDto> LoginUser(UserDto userDto);

    }
}


