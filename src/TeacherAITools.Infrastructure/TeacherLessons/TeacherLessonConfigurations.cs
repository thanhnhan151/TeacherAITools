using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.TeacherLessons
{
    public class TeacherLessonConfigurations : IEntityTypeConfiguration<TeacherLesson>
    {
        public void Configure(EntityTypeBuilder<TeacherLesson> builder)
        {
            builder.ToTable("TeacherLesson");

            builder.HasKey(u => u.TeacherLessonId);

            builder.HasOne(u => u.User)
                .WithMany(u => u.TeacherLessons)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Prompt)
                .WithMany(u => u.TeacherLessons)
                .HasForeignKey(u => u.PromptId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
