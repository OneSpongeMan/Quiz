using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface IResultService
    {
        Result GetResult(Guid id);
        List<Result> GetQuizzResults(Guid quizzId);
        List<Result> GetUserResults(string userId);
        bool CreateResult(Result result);
        bool DeleteResult(Guid id);
        bool UpdateResult(Result result);
    }
}
