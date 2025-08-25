using System.ComponentModel.DataAnnotations;

namespace Quiz.API.DTO
{
    public class UserDTO
    {
        [Key]
        public required String Id { get; set; }
        public required String UserName { get; set; }
        public Int32 AccessFailedCount { get; set; }
        public String? Email { get; set; }
        public Boolean EmailConfirmed { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public String? PhoneNumber { get; set; }
        public Boolean PhoneNumberConfirmed { get; set; }
        public Boolean TwoFactorEnabled { get; set; }

        public DateTime? LatestLogin { get; set; }
        public float AverageScore { get; set; }
        public virtual ICollection<UserDTO>? Users { get; set; }
    }
}
