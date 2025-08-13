using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface IResultLoader
    {
        //Result GetResult(Guid id, string userId);
        Result GetResult(Guid id);
        Result GetUserQuizResult(Guid quizzId, string userId);
        List<Result> GetQuizzResults(Guid quizzId);
        List<Result> GetUserResults(string userId);
        bool CreateResult(Result result);
        bool DeleteResult(Guid id);
        bool UpdateResult(Result result);
    }
}
