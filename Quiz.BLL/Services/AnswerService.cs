using Quiz.Shared.Models;
using Quiz.Shared.Interfaces;

namespace Quiz.BLL.Services
{
    public class AnswerService : IAnswerService
    {
        private IAnswerLoader _answerLoader;
        private IQuestionLoader _questionLoader;

        public AnswerService(IAnswerLoader answerLoader, IQuestionLoader questionLoader)
        {
            _answerLoader = answerLoader;
            _questionLoader = questionLoader;
        }

        public Answer GetAnswer(Guid id)
        {
            return _answerLoader.GetAnswer(id);
        }

        public List<Answer> GetAllAnswers(Guid questionId)
        {
            var question = _questionLoader.GetQuestion(questionId);
            return _answerLoader.GetAllAnswers(question);
        }

        public List<Answer> GetRightAnswers(Guid questionId)
        {
            var question = _questionLoader.GetQuestion(questionId);
            return _answerLoader.GetRightAnswers(question);
        }

        public bool ValidateAnswers(Guid questionId)
        {
            var question = _questionLoader.GetQuestion(questionId);
            var items = _answerLoader.GetRightAnswers(question);
            if (items != null)
            {
                return true;
            }
            return false;
        }

        public bool CreateAnswer(Question question, Answer answer)
        {
            answer.Id = Guid.NewGuid();
            answer.QuestionId = question.Id;
            return _answerLoader.CreateAnswer(question, answer);
        }

        public bool DeleteAnswer(Guid id)
        {
            return _answerLoader.DeleteAnswer(id);
        }

        public bool UpdateAnswer(Answer answer)
        {
            return _answerLoader.UpdateAnswer(answer);
        }

        public bool CreateAnswer(User author, Question question, Answer answer)
        {
            if (author.Id == answer.Question.Quizz.AuthorId)
            {
                answer.Id = Guid.NewGuid();
                answer.QuestionId = question.Id;
                return _answerLoader.CreateAnswer(question, answer);
            }
            return false;
        }

        public bool DeleteAnswer(User author, Guid id)
        {
            return _answerLoader.DeleteAnswer(author, id);
        }

        public bool UpdateAnswer(User author, Answer answer)
        {
            return _answerLoader.UpdateAnswer(author, answer);
        }
    }
}
