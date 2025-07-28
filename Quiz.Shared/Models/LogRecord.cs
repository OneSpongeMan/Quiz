using System.ComponentModel.DataAnnotations;

namespace Quiz.Shared.Models
{
    public class LogRecord
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid QuizId { get; set; }
        public Quizz Quizz { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LogSummary { get; set; }

        public LogRecord() { }

        public LogRecord(Guid id, Guid userId, Guid quizId, DateTime createdDate, string logSummary)
        {
            this.Id = id;
            this.UserId = userId;
            this.QuizId = quizId;
            this.CreatedDate = createdDate;
            this.LogSummary = logSummary;
        }
    }
}
