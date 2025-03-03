using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Lessons
{
    public class WeekConfigurations : IEntityTypeConfiguration<Week>
    {
        public void Configure(EntityTypeBuilder<Week> builder)
        {
            builder.ToTable("Week");

            builder.HasKey(w => w.WeekId);
        }
    }
}
