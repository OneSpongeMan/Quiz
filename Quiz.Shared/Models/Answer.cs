using System.ComponentModel.DataAnnotations;

namespace Quiz.Shared.Models
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }

        public Answer() { }

        public Answer(Guid id, Guid questionId, string text, int score)
        {
            this.Id = id;
            this.QuestionId = questionId;
            this.Text = text;
            this.Score = score;
        }
    }
}
