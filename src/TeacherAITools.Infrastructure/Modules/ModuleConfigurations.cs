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
                .HasMaxLength(50);

            builder.Property(m => m.Desciption)
                .HasMaxLength(50);

            builder.HasOne(m => m.Curriculum)
                .WithMany(m => m.Modules)
                .HasForeignKey(m => m.CurriculumId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
