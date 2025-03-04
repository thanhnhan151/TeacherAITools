using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Notifications
{
    class NotificationTypeConfigurations : IEntityTypeConfiguration<NotificationType>
    {
        public void Configure(EntityTypeBuilder<NotificationType> builder)
        {
            builder.ToTable("NotificationType");

            builder.HasKey(n => n.NotificationTypeId);

            builder.Property(n => n.NotificationTypeName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(n => n.Description)
                .HasMaxLength(50);
        }
    }
}
