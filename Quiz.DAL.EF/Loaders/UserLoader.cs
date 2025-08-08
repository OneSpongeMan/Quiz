using Microsoft.EntityFrameworkCore;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Loaders
{
    public class UserLoader : IUserLoader
    {
        private ApplicationContext _applicationContext;

        public UserLoader(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public User GetUser(string id)
        {
            return _applicationContext.Users
                .Include(q => q.Quizzes)
                .ThenInclude(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .Where(q => q.Id == id)
                .FirstOrDefault();
        }

        public User GetValidUser(string userName, string passwordHash)
        {
            return _applicationContext.Users
                .Include(q => q.Quizzes)
                .ThenInclude(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .Where(q => q.UserName == userName && q.PasswordHash == passwordHash)
                .FirstOrDefault();
        }

        public List<User> GetUsersByName(string userName)
        {
            return _applicationContext.Users
                .Include(q => q.Quizzes)
                .ThenInclude(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .Where(q => q.UserName == userName)
                .ToList();
        }

        public List<User> GetAllUsers()
        {
            return _applicationContext.Users
                .Include(q => q.Quizzes)
                .ThenInclude(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .ToList();
        }

        public List<User> GetAllUsersSorted()
        {
            return Sort(GetAllUsers());
        }

        public bool CreateUser(User user)
        {
            if (GetUser(user.Id) == null)
            {
                _applicationContext.Users.Add(user);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteUser(string id)
        {
            var item = GetUser(id);
            if (item != null)
            {
                _applicationContext.Users.Remove(item);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateUser(User user)
        {
            var item = GetUser(user.Id);
            if (item != null)
            {
                item.UserName = user.UserName;
                item.LatestLogIn = DateTime.Now;
                item.PasswordHash = user.PasswordHash;

                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        private List<User> Sort(List<User> users)
        {
            return (from e in users
                    orderby e.UserName, e.AverageScore
                    select e).ToList();
        }
    }
}
