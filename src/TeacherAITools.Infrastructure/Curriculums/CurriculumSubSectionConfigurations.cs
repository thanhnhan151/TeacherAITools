using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Curriculums
{
    public class CurriculumSubSectionConfigurations : IEntityTypeConfiguration<CurriculumSubSection>
    {
        public void Configure(EntityTypeBuilder<CurriculumSubSection> builder)
        {
            builder.ToTable("CurriculumSubSection");

            builder.HasKey(c => c.CurriculumSubSectionId);

            builder.Property(c => c.CurriculumSubSectionName)
                .HasMaxLength(50);

            builder.HasOne(c => c.CurriculumSection)
                .WithMany(c => c.CurriculumSubSections)
                .HasForeignKey(c => c.CurriculumSectionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
