using Microsoft.EntityFrameworkCore;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Common.Persistence
{
    public class TeacherAIToolsDbContext : DbContext
    {
        private readonly AuditableEntitiesInterceptor _auditableEntitiesInterceptor;

        //public TeacherAIToolsDbContext()
        //{
        //}

        public TeacherAIToolsDbContext(
            DbContextOptions<TeacherAIToolsDbContext> options,
            AuditableEntitiesInterceptor auditableEntitiesInterceptor)
        : base(options)
        {
            _auditableEntitiesInterceptor = auditableEntitiesInterceptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitiesInterceptor);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<SchoolYear> SchoolYears { get; set; }
        public DbSet<Curriculum> Curriculums { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonType> LessonTypes { get; set; }
        public DbSet<Week> Weeks { get; set; }
        //public DbSet<Requirement> Requirements { get; set; }
        //public DbSet<SchoolSupply> SchoolSupplies { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
        //public DbSet<Period> Periods { get; set; }
        //public DbSet<PeriodDetail> PeriodDetails { get; set; }
        public DbSet<LessonHistory> LessonHistories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Prompt> Prompts { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<TeacherLesson> TeacherLessons { get; set; }
        public DbSet<CurriculumActivity> CurriculumActivities { get; set; }
        public DbSet<CurriculumTopic> CurriculumTopics { get; set; }
        public DbSet<CurriculumSection> CurriculumSections { get; set; }
        public DbSet<CurriculumSubSection> CurriculumSubSections { get; set; }
        public DbSet<CurriculumDetail> CurriculumDetails { get; set; }
        public DbSet<CurriculumFeedback> CurriculumFeedbacks { get; set; }
        public DbSet<StartUp> StartUps { get; set; }
        public DbSet<KnowLedge> KnowLedges { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<Apply> Applies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeacherAIToolsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
