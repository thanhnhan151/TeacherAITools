using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Lessons
{
    public class LessonConfigurations : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lesson");

            builder.HasKey(l => l.LessonId);

            builder.Property(l => l.Name)
                .HasMaxLength(50);

            builder.Property(l => l.Description)
                .HasMaxLength(50);

            builder.HasOne(l => l.LessonType)
                .WithMany(l => l.Lessons)
                .HasForeignKey(l => l.LessonTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.Week)
                .WithMany(l => l.Lessons)
                .HasForeignKey(l => l.WeekId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.Requirement)
                .WithMany(l => l.Lessons)
                .HasForeignKey(l => l.RequirementId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.SchoolSupply)
                .WithMany(l => l.Lessons)
                .HasForeignKey(l => l.SchoolSupplyId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.Note)
                .WithMany(l => l.Lessons)
                .HasForeignKey(l => l.NoteId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.Module)
                .WithMany(l => l.Lessons)
                .HasForeignKey(l => l.ModuleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.User)
                .WithMany(l => l.Lessons)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
