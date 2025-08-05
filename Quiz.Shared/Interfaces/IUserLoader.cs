using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface IUserLoader
    {
        User GetUser(string id);
        User GetValidUser(string userName, string passwordHash);
        List<User> GetAllUsers();
        List<User> GetAllUsersSorted();
        List<User> GetUsersByName(string userName);
        bool CreateUser(User user);
        bool DeleteUser(string id);
        bool UpdateUser(User user);
    }
}
