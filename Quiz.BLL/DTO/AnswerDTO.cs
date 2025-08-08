using System.ComponentModel.DataAnnotations;

namespace Quiz.BLL.DTO
{
    public class AnswerDTO
    {
        [Key]
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public int Score { get; set; }

        public Guid QuestionId { get; set; }
        public QuestionDTO? Question { get; set; }
    }
}
