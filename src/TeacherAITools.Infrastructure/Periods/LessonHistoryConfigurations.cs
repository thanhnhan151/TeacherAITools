using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Periods
{
    public class LessonHistoryConfigurations : IEntityTypeConfiguration<LessonHistory>
    {
        public void Configure(EntityTypeBuilder<LessonHistory> builder)
        {
            builder.ToTable("LessonHistory");

            builder.HasKey(l => l.Id);

            //builder.HasOne(l => l.PeriodDetail)
            //    .WithMany(l => l.LessonHistories)
            //    .HasForeignKey(l => l.PeriodDetailId)
            //    .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.TeacherLesson)
                .WithMany(l => l.LessonHistories)
                .HasForeignKey(l => l.TeacherLessonId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
