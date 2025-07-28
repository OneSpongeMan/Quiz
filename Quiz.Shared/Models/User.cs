using System.ComponentModel.DataAnnotations;

namespace Quiz.Shared.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual Role Role { get; set; }
        public Guid RoleId { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LatestLogIn { get; set; }
        public virtual UserStats Stats { get; set; }
        public Guid StatsId { get; set; }

        public User() { }

        public User(Guid id, string name, Guid roleId, string passwordHash)
        {
            this.Id = id;
            this.Name = name;
            this.RoleId = roleId;
            this.PasswordHash = passwordHash;
        }
    }
}
