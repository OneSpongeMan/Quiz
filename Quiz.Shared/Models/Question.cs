using System.ComponentModel.DataAnnotations;

namespace Quiz.Shared.Models
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }
        public Quizz Quizz { get; set; }
        public Guid QuizId { get; set; }
        public string Text { get; set; }
        public byte[]? Image { get; set; }
        public string AnswerType { get; set; }
        public virtual ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
        public DateTime UpdatedDate { get; set; }

        public Question() { }

        public Question(Guid id, Guid quizId, string text, byte[]?image, string answerType)
        {
            this.Id = id;
            this.QuizId = quizId;
            this.Text = text;
            this.AnswerType = answerType;
            this.Image = image;
            this.AnswerType = AnswerType;

        }
    }
}
