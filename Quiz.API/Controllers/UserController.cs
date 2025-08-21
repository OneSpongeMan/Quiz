using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.API.DTO;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;


namespace Quiz.API.Controllers
{
    [Route("api/user-management")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("id/{id}")]
        public UserDTO GetUser(string id)
        {
            return _mapper.Map<UserDTO>(_userService.GetUser(id));
        }

        [HttpGet]
        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _userService.GetAllUsers()
                .Select(q => _mapper.Map<UserDTO>(q));
        }

        [HttpGet("sorted")]
        public IEnumerable<UserDTO> GetAllUsersSorted()
        {
            return _userService.GetAllUsers()
                .Select(q => _mapper.Map<UserDTO>(q));
        }

        [HttpGet("name/{userName}")]
        public IEnumerable<UserDTO> GetUsersByName(string userName)
        {
            return _userService.GetUsersByName(userName)
                .Select(q => _mapper.Map<UserDTO>(q));
        }

        [HttpPost("password/{password}")]
        public bool CreateUser([FromBody] UserDTO user, string password)
        {
            var createdUser = _mapper.Map<User>(user);
            createdUser.PasswordHash = password;
            return _userService.CreateUser(createdUser);
        }

        [HttpDelete("{id}")]
        public bool DeleteUser(string id)
        {
            return _userService.DeleteUser(id);
        }

        [HttpPut]
        public bool UpdateUser([FromBody] UserDTO user)
        {
            return _userService.UpdateUser(_mapper.Map<User>(user));
        }
    }
}
