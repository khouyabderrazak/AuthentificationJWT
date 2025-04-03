using Authentification.JWT.DAL.Data;
using Authentification.JWT.DAL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification.JWT.Service.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _hasher;
        public UserRepository(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _hasher = new PasswordHasher<User>();
        }
        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _db.Users.FirstOrDefaultAsync(us => us.Username.Equals(username));

            return _mapper.Map<UserDto>(user);
        }


        public async Task<UserDto> RegisterUserAsync(string username, string email, string password)
        {
            var user = new User()
            {
                Username = username,
                Email = email,
                PasswordHash = password
            };

            user.PasswordHash = _hasher.HashPassword(user, user.PasswordHash);

             await _db.Users.AddAsync(user);

            _db.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        public  bool VerifyPassword(UserDto user, string password)
        {
            return VerifyPasswordFunc(user,password);
        }


        public async Task<int> GetIdUser(UserDto user)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(ur =>

            ur.Username == user.Username

            );



            if (existingUser is not null && VerifyPasswordFunc(_mapper.Map<UserDto>(existingUser),user.PasswordHash))
                return existingUser.Id;

            return -1;

        }

        public async Task<User> GetUserById(int userId)
        {
            return await _db.Users.FirstOrDefaultAsync(ur =>ur.Id == userId);

        }


        private bool VerifyPasswordFunc(UserDto user, string password)
        {
            var result = _hasher.VerifyHashedPassword(_mapper.Map<User>(user), user.PasswordHash, password);

            return result == PasswordVerificationResult.Success ? true : false;
        }
    }
}
