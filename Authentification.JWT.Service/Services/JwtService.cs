using Authentification.JWT.DAL.Models;
using Authentification.JWT.DAL.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentification.JWT.Service.Services
{
    public class JwtService : IJwtService
    {


        private readonly string _key = "abddsjsjdsdjkdsjkdsjdsjabddsjsjdsdjkdsjkdsjdsjabddsjsjdsdjkdsjkdsjdsjabddsjsjdsdjkdsjkdsjdsjabddsjsjdsdjkdsjkdsjdsjabddsjsjdsdjkdsjkdsjdsj";


        //public JwtService(Configuration config)
        //{
        //    _key = key;
        //}
        private readonly IUserRepository _userRepository;
        public JwtService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> GenerateToken(int userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if(user is not null)
               return CreateJwtToken(user,_key);

            return null;
        }

        private string CreateJwtToken(User user,string JwtKey)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtKey);



            var identity = new ClaimsIdentity(
                new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }
            );

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
