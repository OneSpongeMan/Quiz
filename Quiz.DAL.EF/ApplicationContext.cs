using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Quiz.DAL.EF.Configuration;
using Quiz.Shared.Models;
using System;
using System.Text.RegularExpressions;

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
            //Database.EnsureCreated();
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
            WriteChangesInLog();

            return base.SaveChanges();
        }

        private void WriteChangesInLog()
        {
            var changesFullInfo = ChangeTracker.DebugView.LongView.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();

            for ( int i = 0; i < changesFullInfo.Count(); i++)
            {
                if (changesFullInfo[i].Split(' ')[0] == "Question" ||
                    changesFullInfo[i].Split(' ')[0] == "Answer")
                {
                    string metaData = DiscoverEntityMetadata(changesFullInfo, i);
                    string changesData = DiscoverEntityChanges();

                    var pattern = @"(\w+):\s*([^\n]+)";
                    var metaMatches = Regex.Matches(metaData, pattern);
                    var changesMatches = Regex.Matches(changesData, pattern);

                    var logRecord = new LogRecord()
                    {
                        Id = Guid.NewGuid(),
                        QuizzId = Guid.Parse(metaMatches[0].Groups[2].Value),
                        UserId = metaMatches[1].Groups[2].Value,
                        CreatedDate = DateTime.Now,
                        LogSummary = changesMatches[0].Groups[2].Value
                    };
                }
            }
        }

        private string DiscoverEntityChanges()
        {
            // Поиск изменений
            var changes = "";

            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        var originalValues = entry.OriginalValues;
                        var currentValues = entry.CurrentValues;
                        var changedProperties = new List<string>();

                        foreach (var property in originalValues.Properties)
                        {
                            var originalValue = originalValues[property];
                            var currentValue = currentValues[property];

                            if (!Equals(originalValue, currentValue))
                            {
                                changedProperties.Add($"{property}: {originalValue} -> {currentValue}\n");
                            }
                        }

                        changes = ($"[Update] {entry.Entity.GetType().Name}: {changedProperties.ToString()}\n");
                        break;

                    case EntityState.Added:
                        changes = ($"[Create] {entry.Entity.GetType().Name}\n");
                        break;

                    case EntityState.Deleted:
                        changes = ($"[Delete] {entry.Entity.GetType().Name}\n");
                        break;

                    default:
                        break;
                }
            }

            return changes;
        }

        private string DiscoverEntityMetadata(List<string> changesFullInfo, int entityStartIndex)
        {
            // Поиск метаданных изменений
            var entityInfo = new List<string>() { changesFullInfo[entityStartIndex] };
            var metaInfo = "";

            for (int i = entityStartIndex + 1; i < changesFullInfo.Count; i++)
            {
                if (changesFullInfo[i][0] != ' ')
                {
                    break;
                }
                entityInfo.Add(changesFullInfo[i]);
            }

            var splitEntityInfo = entityInfo.Select(q => q.Split(new char[] { ' ', ':', '\'', '\r', '{', '}', '[', ']',  }, StringSplitOptions.RemoveEmptyEntries).ToList()).ToList();
            var quizID = new Guid();
            var questionID = new Guid();
            var authorID = "";

            for (int i = 0; i < splitEntityInfo.Count; i++)
            {
                for (int j = 0; j < splitEntityInfo[i].Count; j++)
                {
                    switch (splitEntityInfo[i][j])
                    {
                        case "QuizzId":
                            quizID = Guid.Parse(splitEntityInfo[i][j + 1]);
                            authorID = Quizzes.Where(x => x.Id == quizID).FirstOrDefault().AuthorId;
                            metaInfo += ($"quizID: {quizID}\nauthorID: {authorID}\n");
                            break;

                        case "QuestionId":
                            questionID = Guid.Parse(splitEntityInfo[i][j + 1]);
                            metaInfo += ($"questionID: {questionID}\n");
                            break;

                        default:
                            break;
                    }
                }
            }

            return metaInfo;
        }
    }
}
