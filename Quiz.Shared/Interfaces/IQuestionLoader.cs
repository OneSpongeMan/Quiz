using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface IQuestionLoader
    {
        Question GetQuestion(Guid id);
        List<Question> GetAllQuestions(Quizz quizz);
        bool CreateQuestion(Quizz quizz, Question question);
        bool DeleteQuestion(Guid id);
        bool UpdateQuestion(Question question);
        bool DeleteQuestion(User author, Guid id);
        bool UpdateQuestion(User author, Question question);
    }
}
