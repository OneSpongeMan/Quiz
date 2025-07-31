using System.ComponentModel.DataAnnotations;

namespace Quiz.Shared.Models
{
    public class LogRecord
    {
        [Key]
        public Guid Id { get; set; }        
        public DateTime CreatedDate { get; set; }
        public string LogSummary { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public Guid QuizzId { get; set; }
        public Quizz Quizz { get; set; }

        public LogRecord() { }

        public LogRecord(Guid id, string userId, Guid quizId, DateTime createdDate, string logSummary)
        {
            this.Id = id;
            this.UserId = userId;
            this.QuizzId = quizId;
            this.CreatedDate = createdDate;
            this.LogSummary = logSummary;
        }
    }
}
