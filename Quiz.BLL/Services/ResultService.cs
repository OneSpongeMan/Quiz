using Quiz.Shared.Models;
using Quiz.Shared.Interfaces;

namespace Quiz.BLL.Services
{
    public class ResultService : IResultService
    {
        private IResultLoader _resultLoader;
        private IQuestionLoader _questionLoader;
        private IAnswerLoader _answerLoader;


        public ResultService(IResultLoader resultLoader, IQuestionLoader questionLoader, IAnswerLoader answerLoader)
        {
            _resultLoader = resultLoader;
            _questionLoader = questionLoader;
            _answerLoader = answerLoader;
        }

        public Result GetResult(Guid id)
        {
            return _resultLoader.GetResult(id);
        }

        public List<Result> GetQuizzResults(Guid quizzId)
        {
            return _resultLoader.GetQuizzResults(quizzId);
        }

        public List<Result> GetUserResults(string userId)
        {
            return _resultLoader.GetUserResults(userId);
        }

        public void ScoringPoints(List<Guid> answerIds, Guid questionId, string userId)
        {
            var question = _questionLoader.GetQuestion(questionId);
            var rightAnswers = _answerLoader.GetRightAnswers(question);

            var result = _resultLoader.GetUserQuizResult(question.QuizzId, userId);

            foreach (var answerId in answerIds)
            {
                var answer = _answerLoader.GetAnswer(answerId);
                result.ScoredPoints += answer.Score;
            }
        }

        //public AquiringRightAnswer()

        public bool IsResultValid(Guid id)
        {
            var result = _resultLoader.GetResult(id);

            if (result.Finish > result.Quizz.UpdatedDate)
            {
                return true;
            }

            return false;
        }

        public bool CreateResult(Result result)
        {
            result.Id = Guid.NewGuid();
            return _resultLoader.CreateResult(result);
        }

        public bool DeleteResult(Guid id)
        {
            return _resultLoader.DeleteResult(id);
        }

        public bool UpdateResult(Result result)
        {
            return _resultLoader.UpdateResult(result);
        }        
    }
}
