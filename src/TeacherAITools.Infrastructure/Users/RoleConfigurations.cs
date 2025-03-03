using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Users
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.HasKey(r => r.RoleId);

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(26);
        }
    }
}
