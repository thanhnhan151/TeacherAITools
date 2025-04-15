using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Curriculums
{
    public class CurriculumConfigurations : IEntityTypeConfiguration<Curriculum>
    {
        public void Configure(EntityTypeBuilder<Curriculum> builder)
        {
            builder.ToTable("Curriculum");

            builder.HasKey(c => c.CurriculumId);

            builder.Property(c => c.Name)
                .HasMaxLength(20);

            builder.Property(c => c.Description)
                .HasMaxLength(150);

            builder.HasOne(u => u.SchoolYear)
                .WithMany(r => r.Curriculums)
                .HasForeignKey(u => u.SchoolYearId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Grade)
                .WithMany(r => r.Curriculums)
                .HasForeignKey(u => u.GradeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
