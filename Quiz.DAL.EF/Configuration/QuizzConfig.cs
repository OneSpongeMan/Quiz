using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Configuration
{
    public class QuizzConfig : IEntityTypeConfiguration<Quizz>
    {
        public void Configure(EntityTypeBuilder<Quizz> builder)
        {
            builder.ToTable("Quizzes");

            builder.HasOne(q => q.Author)
                .WithMany(p => p.Quizzes)
                .HasForeignKey(q => q.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(q => q.Name).IsRequired().HasMaxLength(200);
            builder.Property(q => q.Description).IsRequired().HasMaxLength(1000);
        }
    }
}
