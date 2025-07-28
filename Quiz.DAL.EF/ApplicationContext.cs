using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Quizz> Quizzes { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;

    }
}
