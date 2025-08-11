using Quiz.Shared.Models;
using Quiz.Shared.Interfaces;

namespace Quiz.BLL.Services
{
    public class UserService : IUserService
    {
        private IUserLoader _userLoader;

        public UserService(IUserLoader userLoader)
        {
            _userLoader = userLoader;
        }

        public User GetUser(string id)
        {
            return _userLoader.GetUser(id);
        }

        public User GetValidUser(string userName, string passwordHash)
        {
            return _userLoader.GetValidUser(userName, passwordHash);
        }

        public List<User> GetAllUsers()
        {
            return _userLoader.GetAllUsers();
        }

        public List<User> GetAllUsersSorted()
        {
            return _userLoader.GetAllUsersSorted();
        }

        public List<User> GetUsersByName(string userName)
        {
            return _userLoader.GetUsersByName(userName);
        }

        public bool CreateUser(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            return _userLoader.CreateUser(user);
        }

        public bool DeleteUser(string id)
        {
            return _userLoader.DeleteUser(id);
        }
        public bool UpdateUser(User user)
        {
            return _userLoader.UpdateUser(user);
        }
    }
}
