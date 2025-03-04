using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Citites
{
    public class WardConfigurations : IEntityTypeConfiguration<Ward>
    {
        public void Configure(EntityTypeBuilder<Ward> builder)
        {
            builder.ToTable("Ward");

            builder.HasKey(w => w.WardId);

            builder.Property(w => w.WardName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(w => w.District)
                .WithMany(w => w.Wards)
                .HasForeignKey(w => w.DistrictId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
