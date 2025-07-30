using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Shared.Models
{
    public class User : IdentityUser
    {
        public DateTime? LatestLogIn { get; set; }
        public float AverageScore { get; set; }

        //public Guid? StatsId { get; set; }
        //public virtual UserStats? Stats { get; set; }

        public virtual ICollection<Quizz> Quizzes { get; set; }
    }
}
