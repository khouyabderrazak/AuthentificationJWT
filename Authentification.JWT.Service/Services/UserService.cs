using Authentification.JWT.DAL.Models;
using Authentification.JWT.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification.JWT.Service.Services
{
    public class UserService

    {

        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)

        {

            _userRepository = userRepository;

        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
 
        public async Task<UserDto> RegisterUserAsync(string username, string email, string password)
        {
            return await _userRepository.RegisterUserAsync(username, email, password);
        }
 
        public bool VerifyPassword(UserDto user, string password)
        {
            return _userRepository.VerifyPassword(user,password);
        }

        public async Task<int> GetIdUser(UserDto userDto)
        {
            return await _userRepository.GetIdUser(userDto);
        }
       


    }
}
