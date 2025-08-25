using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Quiz.API.DTO;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;
using System.Security.Claims;
using System.Text;

namespace Quiz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AuthController(IConfiguration configuration, IUserService userService, IRoleService roleService)
        {
            _configuration = configuration;
            _userService = userService;
            _roleService = roleService;
        }

        [AllowAnonymous]
        [HttpPost("Auth")]
        public IActionResult Auth([FromBody] Login authData)
        {
            var validUser = _userService.GetValidUser(authData.UserName, authData.Password);
            if (validUser != null)
            {
                var token = GenerateJwtToken(validUser);
                return Ok(new { Token = token, Message = $"Welcome, {validUser.UserName}" });
            }
            return BadRequest("Pass the valid username and pasword");
        }

        private string GenerateJwtToken(User user)
        {
            return "Somenthing";
        }
    }
}
