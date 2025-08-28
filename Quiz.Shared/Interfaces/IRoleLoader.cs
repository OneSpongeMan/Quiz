using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface IRoleLoader
    {
        Role GetRole(string id);
        List<Role> GetAllRoles();
        List<Role> GetUserRoles(string userId);
        bool CreateRole(Role role);
        bool DeleteRole(string id);
        bool UpdateRole(Role role);
    }
}
