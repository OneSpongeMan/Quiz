using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface IRoleService
    {
        Role GetRole(string id);
        List<Role> GetAllRoles();
        //bool UserHasRole();
        bool CreateRole(Role role);
        bool DeleteRole(string id);
        bool UpdateRole(Role role);
    }
}
