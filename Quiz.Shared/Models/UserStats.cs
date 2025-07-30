using System.ComponentModel.DataAnnotations;

namespace Quiz.Shared.Models
{
    // Предположительно не будет задействована
    // Поскольку находящиеся в ней данные можно получить
    // из запросов к БД
    public class UserStats
    {
        [Key]
        public Guid Id { get; set; }
        public virtual ICollection<Guid> AuthoredQuizzes { get; set; } = new HashSet<Guid>();
        public virtual ICollection<Guid> CompletedQuizzes { get; set; } = new HashSet<Guid>();
        public float AverageScore { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public UserStats() { }

        public UserStats(Guid id, string userId)
        {
            this.Id = id;
            this.UserId = userId;
        }
    }
}
