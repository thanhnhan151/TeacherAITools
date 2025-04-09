namespace TeacherAITools.Infrastructure.Periods
{
    //public class PeriodConfigurations : IEntityTypeConfiguration<Period>
    //{
    //    public void Configure(EntityTypeBuilder<Period> builder)
    //    {
    //        builder.ToTable("Period");

    //        builder.HasKey(p => p.Id);

    //        builder.HasOne(p => p.Lesson)
    //            .WithMany(p => p.Periods)
    //            .HasForeignKey(p => p.LessonId)
    //            .OnDelete(DeleteBehavior.SetNull);

    //        builder.HasOne(p => p.User)
    //            .WithMany(p => p.Periods)
    //            .HasForeignKey(p => p.UserId)
    //            .OnDelete(DeleteBehavior.SetNull);
    //    }
    //}
}
