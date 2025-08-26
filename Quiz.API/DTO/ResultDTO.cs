using System.ComponentModel.DataAnnotations;

namespace Quiz.API.DTO
{
    public class ResultDTO
    {
        [Key]
        public Guid Id { get; set; }
        public int ScoredPoints { get; set; }
        public int RightAnswers { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }

        public required string UserId { get; set; }
        public virtual UserDTO? User { get; set; }

        public Guid QuizzId { get; set; }
        public QuizzDTO? Quizz { get; set; }
    }
}
