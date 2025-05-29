using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.LessonDatas
{
    public class StartUpConfigurations : IEntityTypeConfiguration<StartUp>
    {
        public void Configure(EntityTypeBuilder<StartUp> builder)
        {
            builder.ToTable("StartUp");

            builder.HasKey(l => l.StartUpId);

            builder.Property(l => l.Duration)
                .HasMaxLength(15);
        }
    }
}
