using Microsoft.EntityFrameworkCore;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Loaders
{
    public class RoleLoader : IRoleLoader
    {
        private ApplicationContext _applicationContext;

        public RoleLoader(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Role GetRole(string id)
        {
            return _applicationContext.Roles
                .Where(q => q.Id == id)
                .FirstOrDefault();
        }

        public List<Role> GetAllRoles()
        {
            return _applicationContext.Roles.ToList();
        }

        public List<Role> GetUserRoles(string userId)
        {
            return _applicationContext.UserRoles
                .Where(q => q.UserId == userId)
                .Join(_applicationContext.Roles,
                    userRole => userRole.RoleId,
                    role => role.Id,
                    (userRole, role) => role)
                .ToList();
        }

        public bool CreateRole(Role role)
        {
            if (GetRole(role.Id) == null)
            {
                _applicationContext.Roles.Add(role);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteRole(string id)
        {
            var item = GetRole(id);
            if (item != null)
            {
                _applicationContext.Roles.Remove(item);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateRole(Role role)
        {
            var item = GetRole(role.Id);
            if (item != null)
            {
                item.Name = role.Name;

                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
