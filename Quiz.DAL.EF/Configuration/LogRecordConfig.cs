using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Configuration
{
    public class LogRecordConfig : IEntityTypeConfiguration<LogRecord>
    {
        public void Configure(EntityTypeBuilder<LogRecord> builder)
        {
            builder.ToTable("LogRecords");

            builder.HasOne(q => q.User)
                .WithMany()
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(q => q.Quizz)
                .WithMany()
                .HasForeignKey(q => q.QuizzId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(q => new { q.UserId, q.QuizzId })
                .IsUnique();
        }
    }
}
