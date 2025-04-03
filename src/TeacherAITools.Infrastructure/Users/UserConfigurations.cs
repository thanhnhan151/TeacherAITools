using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Users
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Fullname)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(u => u.Address)
                .HasMaxLength(50);

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Manager)
                .WithMany(u => u.Teachers)
                .HasForeignKey(u => u.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.School)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.SchoolId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Ward)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.WardId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Grade)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.GradeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
