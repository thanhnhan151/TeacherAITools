using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Periods
{
    public class PeriodDetailConfigurations : IEntityTypeConfiguration<PeriodDetail>
    {
        public void Configure(EntityTypeBuilder<PeriodDetail> builder)
        {
            builder.ToTable("PeriodDetail");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Period)
                .WithMany(p => p.PeriodDetails)
                .HasForeignKey(p => p.PeriodId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
