using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Configuration
{
    public class AnswerConfig : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");

            builder.HasOne(q => q.Question)
                .WithMany(p => p.Answers)
                .HasForeignKey(q => q.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(q => q.Text).IsRequired().HasMaxLength(50);
            builder.Property(q => q.Score).IsRequired().HasDefaultValue(0);
        }
    }
}
