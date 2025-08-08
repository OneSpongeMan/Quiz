using Microsoft.EntityFrameworkCore;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Loaders
{
    public class QuestionLoader : IQuestionLoader
    {
        private ApplicationContext _applicationContext;

        public QuestionLoader(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Question GetQuestion(Guid id)
        {
            return _applicationContext.Questions
                .Include(q => q.Quizz)
                .ThenInclude(q => q.Author)
                .Where(q => q.Id == id)
                .FirstOrDefault();
        }

        public List<Question> GetAllQuestions(Quizz quizz)
        {
            return _applicationContext.Questions
                .Include(q => q.Quizz)
                .ThenInclude(q => q.Author)
                .Where(q => q.Quizz == quizz)
                .ToList();
        }

        public bool CreateQuestion(Quizz quizz, Question question)
        {
            if (GetQuestion(question.Id) == null)
            {
                _applicationContext.Questions.Add(question);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteQuestion(Guid id)
        {
            var item = GetQuestion(id);
            if (item != null)
            {
                _applicationContext.Questions.Remove(item);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateQuestion(Question question)
        {
            var item = GetQuestion(question.Id);
            if (item != null)
            {
                item.Text = question.Text;
                item.Image = question.Image;
                item.AnswerType = question.AnswerType;

                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteQuestion(User author, Guid id)
        {
            var item = GetQuestion(id);
            if (item != null && item.Quizz.AuthorId == author.Id)
            {
                return DeleteQuestion(id);
            }
            return false;
        }

        public bool UpdateQuestion(User author, Question question)
        {
            var item = GetQuestion(question.Id);
            if (item != null && item.Quizz.AuthorId == author.Id)
            {
                return UpdateQuestion(question);
            }
            return false;
        }
    }
}
