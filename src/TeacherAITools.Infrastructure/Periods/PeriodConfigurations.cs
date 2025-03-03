using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Periods
{
    public class PeriodConfigurations : IEntityTypeConfiguration<Period>
    {
        public void Configure(EntityTypeBuilder<Period> builder)
        {
            builder.ToTable("Period");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Lesson)
                .WithMany(p => p.Periods)
                .HasForeignKey(p => p.LessonId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
