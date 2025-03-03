using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Lessons
{
    public class LessonTypeConfigurations : IEntityTypeConfiguration<LessonType>
    {
        public void Configure(EntityTypeBuilder<LessonType> builder)
        {
            builder.ToTable("LessonType");

            builder.HasKey(l => l.LessonTypeId);

            builder.Property(l => l.LessonTypeName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
