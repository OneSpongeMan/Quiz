using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface IAnswerLoader
    {
        Answer GetAnswer(Guid id);
        List<Answer> GetAllAnswers(Question question);
        List<Answer> GetRightAnswers(Question question);
        bool CreateAnswer(Question question, Answer answer);
        bool DeleteAnswer(Guid id);
        bool UpdateAnswer(Answer answer);
        bool DeleteAnswer(User authpr, Guid id);
        bool UpdateAnswer(User author, Answer answer);
    }
}
