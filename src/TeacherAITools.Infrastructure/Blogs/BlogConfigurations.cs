using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Blogs
{
    public class BlogConfigurations : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blog");

            builder.HasKey(b => b.BlogId);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Body)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(c => c.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.LessonPlan)
                .WithMany(c => c.Blogs)
                .HasForeignKey(c => c.LessonPlanId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.User)
                .WithMany(c => c.Blogs)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
