using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Quiz.Shared.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Quizz Quizz { get; set; }
        public Guid QuizId { get; set; }
        public int ScoredPoints { get; set; }
        public int RightAnswers { get; set; }
        public virtual ICollection<>
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }

        public Result() { }
        public Result(Guid id, Guid userId, Guid quizId, int scoredPoints, int rightAnswers, DateTime start, DateTime finish)
        {
            this.Id = Id;
            this.UserId = userId;
            this.QuizId = quizId;
            this.ScoredPoints = scoredPoints;
            this.RightAnswers = rightAnswers;
            this.Start = start;
            this.Finish = finish;
        }
    }
}
