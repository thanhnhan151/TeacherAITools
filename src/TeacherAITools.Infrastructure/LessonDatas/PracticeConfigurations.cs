using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.LessonDatas
{
    public class PracticeConfigurations : IEntityTypeConfiguration<Practice>
    {
        public void Configure(EntityTypeBuilder<Practice> builder)
        {
            builder.ToTable("Practice");

            builder.HasKey(l => l.PracticeId);

            builder.Property(l => l.Duration)
                .HasMaxLength(15);
        }
    }
}
