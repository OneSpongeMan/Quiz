using Microsoft.EntityFrameworkCore;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Loaders
{
    public class ResultLoader : IResultLoader
    {
        private ApplicationContext _applicationContext;

        public ResultLoader(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Result GetResult(Guid id)
        {
            return _applicationContext.Results
                .Include(q => q.User)
                .Where(q => q.Id == id)
                .FirstOrDefault();
        }

        public List<Result> GetQuizzResults(Guid quizzId)
        {
            return _applicationContext.Results
                .Include(q => q.User)
                .Where(q => q.QuizzId == quizzId)
                .ToList();
        }

        public List<Result> GetUserResults(string userId)
        {
            return _applicationContext.Results
                .Include(q => q.User)
                .Where(q => q.UserId == userId)
                .ToList();
        }

        public bool CreateResult(Result result)
        {
            if (GetResult(result.Id) == null)
            {
                _applicationContext.Results.Add(result);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteResult(Guid id)
        {
            var item = GetResult(id);
            if (item != null)
            {
                _applicationContext.Results.Remove(item);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateResult(Result result)
        {
            var item = GetResult(result.Id);
            if (item != null)
            {
                item.ScoredPoints = result.ScoredPoints;
                item.RightAnswers = result.RightAnswers;
                item.Start = result.Start;
                item.Finish = result.Finish;

                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
