using Microsoft.EntityFrameworkCore;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Common.Persistence
{
    public class TeacherAIToolsDbContext : DbContext
    {
        public TeacherAIToolsDbContext()
        {
        }

        public TeacherAIToolsDbContext(DbContextOptions<TeacherAIToolsDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<SchoolYear> SchoolYears { get; set; }
        public DbSet<Curriculum> Curriculums { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonType> LessonTypes { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<TeachingTool> TeachingTools { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<PeriodDetail> PeriodDetails { get; set; }
        public DbSet<LessonHistory> LessonHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var builder = new ConfigurationBuilder()
            //                      .SetBasePath(Directory.GetCurrentDirectory())
            //                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //IConfigurationRoot configuration = builder.Build();

            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DeployConnection"), sqlOptions =>
            //    sqlOptions.EnableRetryOnFailure(
            //        maxRetryCount: 5,
            //        maxRetryDelay: TimeSpan.FromSeconds(30),
            //        errorNumbersToAdd: null));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeacherAIToolsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
