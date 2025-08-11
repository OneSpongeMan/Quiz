using Quiz.Shared.Models;
using Quiz.Shared.Interfaces;

namespace Quiz.BLL.Services
{
    public class RoleService : IRoleService
    {
        private IRoleLoader _roleLoader;

        public RoleService(IRoleLoader roleLoader)
        {
            _roleLoader = roleLoader;
        }

        public Role GetRole(string id)
        {
            return _roleLoader.GetRole(id);
        }

        public List<Role> GetAllRoles()
        {
            return _roleLoader.GetAllRoles();
        }

        public bool CreateRole(Role role)
        {
            role.Id = Guid.NewGuid().ToString();
            return _roleLoader.CreateRole(role);
        }

        public bool DeleteRole(string id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRole(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
