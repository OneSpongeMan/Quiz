using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface IResultService
    {
        Result GetResult(Guid id);
        List<Result> GetQuizzResults(Guid quizzId);
        bool CreateResult(Result result);
        bool DeleteResult(Guid id);
        bool UpdateResult(Result result);
    }
}
