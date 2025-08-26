using Quiz.Shared.Models;
using Quiz.Shared.Interfaces;

namespace Quiz.BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private IQuestionLoader _questionLoader;
        private IQuizzLoader _quizzLoader;

        public QuestionService(IQuestionLoader questionLoader, IQuizzLoader quizzLoader)
        {
            _questionLoader = questionLoader;
            _quizzLoader = quizzLoader;
        }

        public Question GetQuestion(Guid id)
        {
            return _questionLoader.GetQuestion(id);
        }

        public List<Question> GetAllQuestions(Guid quizzId)
        {
            var quizz = _quizzLoader.GetQuizz(quizzId);
            return _questionLoader.GetAllQuestions(quizz);
        }

        public bool CreateQuestion(Quizz quizz, Question question)
        {
            question.Id = Guid.NewGuid();
            question.QuizzId = quizz.Id;
            return _questionLoader.CreateQuestion(quizz, question);
        }

        public bool DeleteQuestion(Guid id)
        {
            return _questionLoader.DeleteQuestion(id);
        }

        public bool UpdateQuestion(Question question)
        {
            return _questionLoader.UpdateQuestion(question);
        }

        public bool CreateQuestion(User author, Quizz quizz, Question question)
        {
            if (author.Id == question.Quizz.AuthorId)
            {
                question.Id = Guid.NewGuid();
                question.QuizzId = quizz.Id;
                return _questionLoader.CreateQuestion(quizz, question);
            }
            return false;
        }

        public bool DeleteQuestion(User author, Guid id)
        {
            return _questionLoader.DeleteQuestion(author, id);
        }

        public bool UpdateQuestion(User author, Question question)
        {
            return _questionLoader.UpdateQuestion(author, question);
        }
    }
}
