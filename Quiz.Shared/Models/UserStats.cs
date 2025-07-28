using System.ComponentModel.DataAnnotations;

namespace Quiz.Shared.Models
{
    public class UserStats
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual ICollection<Guid> AuthoredQuizzes { get; set; } = new HashSet<Guid>();
        public virtual ICollection<Guid> CompletedQuizzes { get; set; } = new HashSet<Guid>();
        public float Score { get; set; }

        public UserStats() { }

        public UserStats(Guid id, Guid userId)
        {
            this.Id = id;
            this.UserId = userId;
        }
    }
}
