using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Curriculums
{
    public class CurriculumActivityConfigurations : IEntityTypeConfiguration<CurriculumActivity>
    {
        public void Configure(EntityTypeBuilder<CurriculumActivity> builder)
        {
            builder.ToTable("CurriculumActivity");

            builder.HasKey(c => c.CurriculumActivityId);

            builder.HasOne(c => c.Curriculum)
                .WithMany(c => c.CurriculumActivities)
                .HasForeignKey(c => c.CurriculumId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
