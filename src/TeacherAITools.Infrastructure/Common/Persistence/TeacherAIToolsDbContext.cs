using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                                  .SetBasePath(Directory.GetCurrentDirectory())
                                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DeployConnection"), sqlOptions =>
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeacherAIToolsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
