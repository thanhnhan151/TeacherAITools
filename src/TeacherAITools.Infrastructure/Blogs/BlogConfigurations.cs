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
                .HasMaxLength(25);

            builder.Property(b => b.Body)
                .HasMaxLength(100);
        }
    }
}
