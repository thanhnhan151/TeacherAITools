using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Curriculums
{
    public class CurriculumSectionConfigurations : IEntityTypeConfiguration<CurriculumSection>
    {
        public void Configure(EntityTypeBuilder<CurriculumSection> builder)
        {
            builder.ToTable("CurriculumSection");

            builder.HasKey(c => c.CurriculumSectionId);

            builder.Property(c => c.CurriculumSectionName)
                .HasMaxLength(50);

            builder.HasOne(c => c.CurriculumTopic)
                .WithMany(c => c.CurriculumSections)
                .HasForeignKey(c => c.CurriculumTopicId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
