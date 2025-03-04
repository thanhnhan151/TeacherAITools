using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Citites
{
    public class DistrictConfigurations : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable("District");

            builder.HasKey(d => d.DistrictId);

            builder.Property(d => d.DistrictName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.City)
                .WithMany(d => d.Districts)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
