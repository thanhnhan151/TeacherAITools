using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Lessons
{
    public class NoteConfigurations : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Note");

            builder.HasKey(n => n.NoteId);

            builder.Property(n => n.Description)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
