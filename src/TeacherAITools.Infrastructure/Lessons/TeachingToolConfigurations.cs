using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Lessons
{
    public class TeachingToolConfigurations : IEntityTypeConfiguration<TeachingTool>
    {
        public void Configure(EntityTypeBuilder<TeachingTool> builder)
        {
            builder.ToTable("TeachingTool");

            builder.HasKey(t => t.TeachingToolId);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
