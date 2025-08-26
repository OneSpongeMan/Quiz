using System.ComponentModel.DataAnnotations;

namespace Quiz.API.DTO
{
    public class QuestionDTO
    {
        [Key]
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public byte[]? Image { get; set; }
        public required string AnswerType { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Guid QuizzId { get; set; }
        public QuizzDTO? Quizz { get; set; }

        public virtual ICollection<AnswerDTO>? Answers { get; set; } = new HashSet<AnswerDTO>();
    }
}
