using Quiz.Shared.Models;
using Quiz.Shared.Interfaces;

namespace Quiz.BLL.Services
{
    public class ResultService : IResultService
    {
        private IResultLoader _resultLoader;

        public ResultService(IResultLoader resultLoader)
        {
            _resultLoader = resultLoader;
        }

        public Result GetResult(Guid id)
        {
            return _resultLoader.GetResult(id);
        }

        public List<Result> GetQuizzResults(Guid quizzId)
        {
            return _resultLoader.GetQuizzResults(quizzId);
        }

        public List<Result> GetUserResults(string userId)
        {
            return _resultLoader.GetUserResults(userId);
        }

        public bool CreateResult(Result result)
        {
            result.Id = Guid.NewGuid();
            return _resultLoader.CreateResult(result);
        }

        public bool DeleteResult(Guid id)
        {
            return _resultLoader.DeleteResult(id);
        }

        public bool UpdateResult(Result result)
        {
            throw new NotImplementedException();
        }
    }
}
