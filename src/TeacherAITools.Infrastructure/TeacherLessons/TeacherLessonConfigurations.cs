using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.TeacherLessons
{
    public class TeacherLessonConfigurations : IEntityTypeConfiguration<TeacherLesson>
    {
        public void Configure(EntityTypeBuilder<TeacherLesson> builder)
        {
            builder.ToTable("LessonPlan");

            builder.HasKey(u => u.LessonPlanId);

            builder.Property(u => u.Duration)
                .HasMaxLength(15);

            builder.HasOne(u => u.User)
                .WithMany(u => u.LessonPlans)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Prompt)
                .WithMany(u => u.LessonPlans)
                .HasForeignKey(u => u.PromptId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
