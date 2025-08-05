using Microsoft.EntityFrameworkCore;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;
using System.Xml.Linq;

namespace Quiz.DAL.EF.Loaders
{
    public class QuizzLoader : IQuizzLoader
    {
        private ApplicationContext _applicationContext;

        public QuizzLoader(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Quizz GetQuizz(Guid id)
        {
            return _applicationContext.Quizzes
                .Include(q => q.Author)
                .Where(q => q.Id == id)
                .FirstOrDefault();
        }

        public List<Quizz> GetAllQuizzes()
        {
            return _applicationContext.Quizzes
                .Include(q => q.Author)
                .ToList();
        }

        public List<Quizz> GetAllQuizzesSorted()
        {
            return Sort(GetAllQuizzes());
        }

        public List<Quizz> GetQuizzByName(string name)
        {
            return _applicationContext.Quizzes
                .Include(q => q.Author)
                .Where (q => q.Name == name)
                .ToList();
        }

        public List<Quizz> GetAllQuizzesByTags(ICollection<string> tags)
        {
            return _applicationContext.Quizzes
                .Include(q => q.Author)
                .Where(q => q.Tags.All(t => tags.Contains(t)))
                .ToList();
        }

        public List<Quizz> GetAllQuizzesByAuthor(string authorId)
        {
            return _applicationContext.Quizzes
                .Include(q => q.Author)
                .Where(q => q.AuthorId == authorId)
                .ToList();
        }

        public List<Quizz> GetAllQuizzesUserCompleted(string userId)
        {
            return _applicationContext.Quizzes
                .Include(q => q.Author)
                .Where(q => _applicationContext.Results
                .Any(r => r.UserId == userId && r.QuizzId == q.Id))
                .ToList();
        }

        public bool CreateQuizz(Quizz quizz)
        {
            if (GetQuizz(quizz.Id) == null)
            {
                _applicationContext.Quizzes.Add(quizz);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteQuizz(Guid id)
        {
            var item = GetQuizz(id);
            if (item != null)
            {
                _applicationContext.Quizzes.Remove(item);
                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateQuizz(Quizz quizz)
        {
            var item = GetQuizz(quizz.Id);
            if (item != null)
            {
                item.Name = quizz.Name;
                item.Description = quizz.Description;
                item.Duration = quizz.Duration;
                item.Tags = quizz.Tags;
                item.UpdatedDate = DateTime.Now;                

                _applicationContext.SaveChanges();
                return true;
            }
            return false;
        }

        private List<Quizz> Sort(List<Quizz> quizzes)
        {
            return (from e in quizzes
                    orderby e.Name, e.Duration, e.Author.Id
                    select e).ToList();
        }
    }
}
