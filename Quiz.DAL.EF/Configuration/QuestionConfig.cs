using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Configuration
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");

            builder.HasOne(q => q.Quizz)
                .WithMany(p => p.Questions)
                .HasForeignKey(q => q.QuizzId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(q => q.Text).IsRequired().HasMaxLength(200);
            builder.Property(q => q.AnswerType).IsRequired().HasMaxLength(50);
        }
    }
}
