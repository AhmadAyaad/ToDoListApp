using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.API.Dtos;
using ToDoList.API.Models;
using ToDoList.API.Repository;

namespace ToDoList.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        readonly IAuthRepository authRepositery;
        readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;
        public AuthController(IAuthRepository repositery, IConfiguration configuration)
        {
            authRepositery = repositery;
            _configuration = configuration;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            userDto.UserName = userDto.UserName.ToLower();
            if (await authRepositery.UserExists(userDto.UserName))
                return BadRequest("user already exists");

            var createdUser = new User
            {
                UserName = userDto.UserName
            };
            await authRepositery.Register(createdUser, userDto.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var user = await authRepositery.Login(userDto.UserName, userDto.Password);

            if (user == null)
                return Unauthorized();

            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier ,  user.UserId.ToString()) ,
                    new Claim(ClaimTypes.Name , userDto.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = creds,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //var userToReturn  = _mapper.Map<UserToReturnDto>(user);
            UserToReturnDto userToReturnDto = new UserToReturnDto()
            {
                UserId = user.UserId,
                Username = user.UserName
            };

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                userToReturnDto
            });
        }
    }
}
