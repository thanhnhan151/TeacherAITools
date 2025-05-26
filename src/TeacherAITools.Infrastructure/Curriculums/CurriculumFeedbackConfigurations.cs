using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Curriculums
{
    public class CurriculumFeedbackConfigurations : IEntityTypeConfiguration<CurriculumFeedback>
    {
        public void Configure(EntityTypeBuilder<CurriculumFeedback> builder)
        {
            builder.ToTable("CurriculumFeedback");

            builder.HasKey(c => c.CurriculumFeedBackId);

            builder.Property(s => s.Body)
                .HasMaxLength(250);

            builder.HasOne(u => u.Curriculum)
                .WithMany(r => r.CurriculumFeedbacks)
                .HasForeignKey(u => u.CurriculumId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.User)
                .WithMany(r => r.CurriculumFeedbacks)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
