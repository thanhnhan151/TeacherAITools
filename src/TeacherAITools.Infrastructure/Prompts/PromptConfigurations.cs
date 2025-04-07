using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Promps
{
    class PromptConfigurations : IEntityTypeConfiguration<Prompt>
    {
        public void Configure(EntityTypeBuilder<Prompt> builder)
        {
            builder.ToTable("Prompt");

            builder.HasKey(p => p.PromptId);

            builder.Property(p => p.Description)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(p => p.Prompts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Lesson)
                .WithMany(p => p.Prompts)
                .HasForeignKey(p => p.LessonId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
