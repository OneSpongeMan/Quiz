using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface IQuizzService
    {
        Quizz GetQuizz(Guid id);
        List<Quizz> GetAllQuizzes();
        List<Quizz> GetAllQuizzesSorted();
        List<Quizz> GetQuizzByName(string name);
        List<Quizz> GetAllQuizzesByTags(ICollection<string> tags);
        List<Quizz> GetAllQuizzesByAuthor(string authorId);
        List<Quizz> GetAllQuizzesUserCompleted(string userId);
        bool CreateQuizz(Quizz quizz);
        bool DeleteQuizz(Guid id);
        bool UpdateQuizz(Quizz quizz);
        bool CreateQuizz(User author, Quizz quizz);
        bool DeleteQuizz(User author, Guid id);
        bool UpdateQuizz(User author, Quizz quizz);
    }
}
