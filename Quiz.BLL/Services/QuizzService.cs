using Quiz.Shared.Models;
using Quiz.Shared.Interfaces;

namespace Quiz.BLL.Services
{
    public class QuizzService : IQuizzService
    {
        private IQuizzLoader _quizzLoader;
        private IResultLoader _resultLoader;

        public QuizzService(IQuizzLoader quizzLoader, IResultLoader resultLoader)
        {
            _quizzLoader = quizzLoader;
            _resultLoader = resultLoader;
        }

        public Quizz GetQuizz(Guid id)
        {
            return _quizzLoader.GetQuizz(id);
        }
        public List<Quizz> GetAllQuizzes()
        {
            return _quizzLoader.GetAllQuizzes();
        }
        public List<Quizz> GetAllQuizzesSorted()
        {
            return _quizzLoader.GetAllQuizzesSorted();
        }

        public List<Quizz> GetQuizzByName(string name)
        {
            return _quizzLoader.GetQuizzByName(name);
        }
        public List<Quizz> GetAllQuizzesByTags(ICollection<string> tags)
        {
            return _quizzLoader.GetAllQuizzesByTags(tags);
        }

        public List<Quizz> GetAllQuizzesByAuthor(string authorId)
        {
            return _quizzLoader.GetAllQuizzesByAuthor(authorId);
        }        

        public List<Quizz> GetAllQuizzesUserCompleted(string userId)
        {
            return _quizzLoader.GetAllQuizzesUserCompleted(userId);
        }

        public bool StartQuizz(Guid id, string userId)
        {
            var userResult = _resultLoader.GetUserQuizResult(id, userId);
            if (userResult == null)
            {
                userResult = new Result();
                userResult.Id = Guid.NewGuid();
                userResult.UserId = userId;
                userResult.QuizzId = id;
                userResult.ScoredPoints = 0;
                userResult.RightAnswers = 0;
                userResult.Start = DateTime.Now;

                _resultLoader.CreateResult(userResult);
                return true;
            }
            return false;
        }

        public bool EndQuizz(Guid id, string userId)
        {
            var userResult = _resultLoader.GetUserQuizResult(id, userId);
            if (userResult != null)
            {
                userResult.Finish = DateTime.Now;

                _resultLoader.UpdateResult(userResult);
                return true;
            }
            return false;
        }

        public bool IsQuizTimeExpired(Guid resultId)
        {
            var result = _resultLoader.GetResult(resultId);
            if (result.Finish.HasValue) { return true; }

            var timeElapsed = DateTime.Now - result.Start;
            return timeElapsed > result.Quizz.Duration;
        }

        public void CompleteQuizIfTimeExpired(Guid resultId)
        {
            var result = _resultLoader.GetResult(resultId);
            if (result.Finish.HasValue) return;

            if (IsQuizTimeExpired(resultId))
            {
                result.Finish = DateTime.Now;
                _resultLoader.UpdateResult(result);
            }
        }

        public bool CreateQuizz(Quizz quizz)
        {
            quizz.Id = Guid.NewGuid();
            return _quizzLoader.CreateQuizz(quizz);
        }

        public bool DeleteQuizz(Guid id)
        {
            return _quizzLoader.DeleteQuizz(id);
        }

        public bool UpdateQuizz(Quizz quizz)
        {
            return _quizzLoader.UpdateQuizz(quizz);
        }

        public bool CreateQuizz(User author, Quizz quizz)
        {
            quizz.Id = Guid.NewGuid();
            quizz.AuthorId = author.Id;
            return _quizzLoader.CreateQuizz(quizz);
        }

        public bool DeleteQuizz(User author, Guid id)
        {
            return _quizzLoader.DeleteQuizz(author, id);
        }

        public bool UpdateQuizz(User author, Quizz quizz)
        {
            return _quizzLoader.UpdateQuizz(author, quizz);
        }
    }
}
