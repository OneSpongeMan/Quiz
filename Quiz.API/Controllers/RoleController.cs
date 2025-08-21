using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.API.DTO;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.API.Controllers
{
    [Route("api/role-management")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet("id/{id}")]
        public Role GetRole(string id)
        {
            return _roleService.GetRole(id);
        }

        [HttpGet]
        public IEnumerable<Role> GetAllRoles()
        {
            return _roleService.GetAllRoles();
        }

        [HttpPost]
        public bool CreateRole([FromBody] Role role)
        {
            return _roleService.CreateRole(role);
        }

        [HttpDelete("{id}")]
        public bool DeleteRole(string id)
        {
            return _roleService.DeleteRole(id);
        }

        [HttpPut]
        public bool UpdateRole([FromBody] Role role)
        {
            return _roleService.UpdateRole(role);
        }
    }
}
