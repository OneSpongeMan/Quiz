using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Quiz.Shared.Models;
using Quiz.DAL.EF.Configuration;

namespace Quiz.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Quizz> Quizzes { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; } = null!;
        public DbSet<Result> Results { get; set; } = null!;
        public DbSet<LogRecord> LogRecords { get; set; } = null!;
        // Не задействуется, смотри описание в Quiz.Shared.Models
        //public DbSet<UserStats> UserStats { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new QuizzConfig());
            builder.ApplyConfiguration(new QuestionConfig());
            builder.ApplyConfiguration(new AnswerConfig());
            builder.ApplyConfiguration(new ResultConfig());
            builder.ApplyConfiguration(new LogRecordConfig());

            //builder.Entity<Quizz>();
            //builder.Entity<Question>();
            //builder.Entity<Answer>();
            //builder.Entity<Result>();
            //builder.Entity<LogRecord>();
            ////builder.Entity<UserStats>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
