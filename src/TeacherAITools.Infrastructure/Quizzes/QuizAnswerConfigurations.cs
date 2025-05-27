using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Quizzes
{
    public class QuizAnswerConfigurations : IEntityTypeConfiguration<QuizAnswer>
    {
        public void Configure(EntityTypeBuilder<QuizAnswer> builder)
        {
            builder.ToTable("QuizAnswer");

            builder.HasKey(q => q.AnswerId);

            builder.Property(q => q.Answer)
                .IsRequired();

            builder.HasOne(q => q.QuizQuestion)
                .WithMany(q => q.QuizAnswers)
                .HasForeignKey(q => q.QuestionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
