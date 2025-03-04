using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Schools
{
    public class SchoolConfigurations : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.ToTable("School");

            builder.HasKey(s => s.SchoolId);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.ImageURL)
                .IsRequired();

            builder.Property(s => s.Address)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(s => s.Ward)
                .WithMany(s => s.Schools)
                .HasForeignKey(s => s.WardId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
