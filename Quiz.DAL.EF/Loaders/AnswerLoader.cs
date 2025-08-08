using Microsoft.EntityFrameworkCore;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Loaders
{
    public class AnswerLoader : IAnswerLoader
    {
        private ApplicationContext _applicationContext;

        public AnswerLoader(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Answer GetAnswer(Guid id)
        {
            return _applicationContext.Answers
                .Include(q => q.Question)
                .ThenInclude(q => q.Quizz)
                .ThenInclude(q => q.Author)
                .Where(q => q.Id == id)
                .FirstOrDefault();
        }

        public List<Answer> GetAllAnswers(Question question)
        {
            return _applicationContext.Answers
                .Include(q => q.Question)
                .ThenInclude(q => q.Quizz)
                .ThenInclude(q => q.Author)
                .Where(q => q.Question == question)
                .ToList();
        }

        public bool CreateAnswer(Question question, Answer answer)
        {
            if (GetAnswer(answer.Id) == null)
            {
                _applicationContext.Answers.Add(answer);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteAnswer(Guid id)
        {
            var item = GetAnswer(id);
            if (item != null)
            {
                _applicationContext.Answers.Remove(item);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateAnswer(Answer answer)
        {
            var item = GetAnswer(answer.Id);
            if (item != null)
            {
                item.Text = answer.Text;
                item.Score = answer.Score;

                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteAnswer(User author,  Guid id)
        {
            var item = GetAnswer(id);
            if (item != null && item.Question.Quizz.AuthorId == author.Id)
            {
                return DeleteAnswer(id);
            }
            return false;
        }

        public bool UpdateAnswer(User author, Answer answer)
        {
            var item = GetAnswer(answer.Id);
            if (item != null && item.Question.Quizz.AuthorId == author.Id)
            {
                return UpdateAnswer(answer);
            }
            return false;
        }
    }
}
