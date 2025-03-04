﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Prompt> Prompts { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeacherAIToolsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
