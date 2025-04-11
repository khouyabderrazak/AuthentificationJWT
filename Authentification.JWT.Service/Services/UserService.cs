using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service.Repository;
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

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
 
        public async Task<User> RegisterUserAsync(string username, string email, string password)
        {
            return await _userRepository.RegisterUserAsync(username, email, password);
        }
 
        public bool VerifyPassword(User user, string password)
        {
            return _userRepository.VerifyPassword(user,password);
        }
        
        public async Task<bool> IsEmailAlreadyExist(string email)
        {
            return await _userRepository.IsEmailAlreadyExist(email);
        }
        
        public async Task<bool> IsUserNameAlreadyExist(string username)
        {
            return await _userRepository.IsUserNameAlreadyExist(username);
        }

    }
}
