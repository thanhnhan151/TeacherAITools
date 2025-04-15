using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Curriculums
{
    public class CurriculumDetailConfigurations : IEntityTypeConfiguration<CurriculumDetail>
    {
        public void Configure(EntityTypeBuilder<CurriculumDetail> builder)
        {
            builder.ToTable("CurriculumDetail");

            builder.HasKey(c => c.CurriculumDetailId);

            builder.HasOne(u => u.Curriculum)
                .WithMany(r => r.CurriculumDetails)
                .HasForeignKey(u => u.CurriculumId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.CurriculumSubSection)
                .WithMany(r => r.CurriculumDetails)
                .HasForeignKey(u => u.CurriculumSubSectionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
