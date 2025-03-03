using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Curriculums
{
    public class SchoolYearConfigurations : IEntityTypeConfiguration<SchoolYear>
    {
        public void Configure(EntityTypeBuilder<SchoolYear> builder)
        {
            builder.ToTable("SchoolYear");

            builder.HasKey(s => s.SchoolYearId);

            builder.Property(s => s.Year)
                .HasMaxLength(11);
        }
    }
}
