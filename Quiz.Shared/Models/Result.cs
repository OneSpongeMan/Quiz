using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Quiz.Shared.Models
{
    public class Result
    {
        [Key]
        public Guid Id { get; set; }        
        public int ScoredPoints { get; set; }
        public int RightAnswers { get; set; }
        //public virtual ICollection<Answer> UserAnswers { get; set; } = new HashSet<Answer>();
        public DateTime Start { get; set; }
        public DateTime? Finish { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public Guid QuizzId { get; set; }
        public Quizz Quizz { get; set; }

        public Result() { }
        public Result(Guid id, string userId, Guid quizId, int scoredPoints, int rightAnswers, DateTime start, DateTime finish)
        {
            this.Id = id;
            this.UserId = userId;
            this.QuizzId = quizId;
            this.ScoredPoints = scoredPoints;
            this.RightAnswers = rightAnswers;
            this.Start = start;
            this.Finish = finish;
        }
    }
}
