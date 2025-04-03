﻿using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service.Services;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Authentification.JWT.WebAPI.Models;
namespace Authentification.JWT.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        private readonly IJwtService _jwtService;

            
        public AuthController(UserService userService, IJwtService jwtService)

        {
            _userService = userService;
            _jwtService = jwtService;
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterModel model)

        {
           var user =  _userService.RegisterUserAsync(model.Username, model.Email, model.PasswordHash);

            return Ok(user);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.GetUserByUsernameAsync(model.Username);
            var res = _userService.VerifyPassword(user, model.PasswordHash);

            if(user is null)
            {
                return NotFound("user name incorrect");
            }
            else if (!res)
            {
                return NotFound("password incorrect");
            }

            var token = await _jwtService.GenerateToken(user.Id);

            return Ok(new { token = token , message = "login avec success"});

        }

        [Authorize]

        [HttpGet("protected")]

        public IActionResult ProtectedEndpoint()

        {

            return Ok("You have accessed a protected endpoint!");

        }
    }
}
