using Microsoft.Extensions.Options;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface IAnswerService
    {
        Answer GetAnswer(Guid id);
        List<Answer> GetAllAnswers(Question question);
        List<Answer> GetRightAnswers(Question question);
        bool ValidateAnswers(Question question);
        bool CreateAnswer(Question question, Answer answer);
        bool DeleteAnswer(Guid id);
        bool UpdateAnswer(Answer answer);
        bool CreateAnswer(User author, Question question, Answer answer);
        bool DeleteAnswer(User author, Guid id);
        bool UpdateAnswer(User author, Answer answer);
    }
}
