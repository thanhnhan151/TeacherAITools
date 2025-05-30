using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Quizzes
{
    public class QuizConfigurations : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("Quiz");

            builder.HasKey(q => q.QuizId);

            builder.Property(q => q.QuizName)
                .IsRequired();

            builder.HasOne(q => q.Lesson)
                .WithMany(q => q.Quizzes)
                .HasForeignKey(q => q.LessonId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(q => q.User)
                .WithMany(q => q.Quizzes)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
