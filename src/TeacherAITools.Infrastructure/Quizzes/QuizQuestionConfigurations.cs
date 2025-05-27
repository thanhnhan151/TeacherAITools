using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Quizzes
{
    public class QuizQuestionConfigurations : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.ToTable("QuizQuestion");

            builder.HasKey(q => q.QuestionId);

            builder.Property(q => q.QuestionName)
                .IsRequired();

            builder.HasOne(q => q.Quiz)
                .WithMany(q => q.QuizQuestions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
