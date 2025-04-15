using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Curriculums
{
    public class CurriculumTopicConfigurations : IEntityTypeConfiguration<CurriculumTopic>
    {
        public void Configure(EntityTypeBuilder<CurriculumTopic> builder)
        {
            builder.ToTable("CurriculumTopic");

            builder.HasKey(c => c.CurriculumTopicId);

            builder.Property(c => c.CurriculumTopicName)
                .HasMaxLength(50);
        }
    }
}
