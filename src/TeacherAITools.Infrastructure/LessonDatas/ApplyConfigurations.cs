using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.LessonDatas
{
    public class ApplyConfigurations : IEntityTypeConfiguration<Apply>
    {
        public void Configure(EntityTypeBuilder<Apply> builder)
        {
            builder.ToTable("Apply");

            builder.HasKey(l => l.ApplyId);

            builder.Property(l => l.Duration)
                .HasMaxLength(15);
        }
    }
}
