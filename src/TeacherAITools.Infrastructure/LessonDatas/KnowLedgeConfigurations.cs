using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.LessonDatas
{
    public class KnowLedgeConfigurations : IEntityTypeConfiguration<KnowLedge>
    {
        public void Configure(EntityTypeBuilder<KnowLedge> builder)
        {
            builder.ToTable("KnowLedge");

            builder.HasKey(l => l.KnowLedgeId);

            builder.Property(l => l.Duration)
                .HasMaxLength(15);
        }
    }
}
