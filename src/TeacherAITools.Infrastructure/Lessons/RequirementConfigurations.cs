using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Lessons
{
    class RequirementConfigurations : IEntityTypeConfiguration<Requirement>
    {
        public void Configure(EntityTypeBuilder<Requirement> builder)
        {
            builder.ToTable("Requirement");

            builder.HasKey(r => r.RequirementId);

            builder.Property(r => r.Description)
                .IsRequired();
        }
    }
}
