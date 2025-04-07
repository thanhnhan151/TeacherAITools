using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Modules
{
    public class ModuleConfigurations : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Module");

            builder.HasKey(m => m.ModuleId);

            builder.Property(m => m.Name)
                .HasMaxLength(150);

            builder.Property(m => m.Desciption)
                .HasMaxLength(150);

            builder.HasOne(u => u.Grade)
                .WithMany(r => r.Modules)
                .HasForeignKey(u => u.GradeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Book)
                .WithMany(r => r.Modules)
                .HasForeignKey(u => u.BookId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(m => m.Curriculum)
                .WithMany(m => m.Modules)
                .HasForeignKey(m => m.CurriculumId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
