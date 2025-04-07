using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Infrastructure.Blogs;
using TeacherAITools.Infrastructure.Citites;
using TeacherAITools.Infrastructure.Curriculums;
using TeacherAITools.Infrastructure.Lessons;
using TeacherAITools.Infrastructure.Modules;
using TeacherAITools.Infrastructure.Notifications;
using TeacherAITools.Infrastructure.Periods;
using TeacherAITools.Infrastructure.Prompts;
using TeacherAITools.Infrastructure.Quizzes;
using TeacherAITools.Infrastructure.Schools;
using TeacherAITools.Infrastructure.Users;

namespace TeacherAITools.Infrastructure.Common.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TeacherAIToolsDbContext _dbContext;

        private readonly ILogger _logger;

        public IUserRepository Users { get; private set; }

        public IModuleRepository Modules { get; private set; }

        public ICurriculumRepository Curriculums { get; private set; }

        public ISchoolRepository Schools { get; private set; }

        public ICityRepository Cities { get; private set; }

        public IDistrictRepository Districts { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public IBlogRepository Blogs { get; private set; }

        public ICommentRepository Comments { get; private set; }

        public ILessonsRepository Lessons { get; private set; }

        public IRoleRepository Roles { get; private set; }

        public INotificationRepository Notifications { get; private set; }

        public IGradeRepository Grades { get; private set; }

        public ILessonTypeRepository LessonTypes { get; private set; }

        public IRequirementRepository Requirements { get; private set; }

        public INoteRepository Notes { get; private set; }

        public ISchoolSupplyRepository SchoolSupplies { get; private set; }

        public IWeekRepository Weeks { get; private set; }

        public ISchoolYearRepository SchoolYears { get; private set; }

        public IBookRepository Books { get; private set; }

        public IPeriodRepository Periods { get; private set; }

        public IPeriodDetailRepository PeriodDetails { get; private set; }

        public IPromptRepository Prompts { get; private set; }

        public IQuizRepository Quizzes { get; private set; }

        public IQuizQuestionRepository QuizQuestions { get; private set; }

        public IQuizAnswerRepository QuizAnswers { get; private set; }

        public UnitOfWork(
            TeacherAIToolsDbContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;

            _logger = loggerFactory.CreateLogger("logs");

            Users = new UserRepository(_dbContext, _logger);

            Modules = new ModuleRepository(_dbContext, _logger);

            Curriculums = new CurriculumRepository(_dbContext, _logger);

            Schools = new SchoolRepository(_dbContext, _logger);

            Cities = new CityRepository(_dbContext, _logger);

            Districts = new DistrictRepository(_dbContext, _logger);

            Categories = new CategoryRepository(_dbContext, _logger);

            Blogs = new BlogRepository(_dbContext, _logger);

            Comments = new CommentRepository(_dbContext, _logger);

            Lessons = new LessonsRepository(_dbContext, _logger);

            Roles = new RoleRepository(_dbContext, _logger);

            Notifications = new NotificationRepository(_dbContext, _logger);

            Grades = new GradeRepository(_dbContext, _logger);

            LessonTypes = new LessonTypeRepository(_dbContext, _logger);

            Requirements = new RequirementRepository(_dbContext, _logger);

            Notes = new NoteRepository(_dbContext, _logger);

            SchoolSupplies = new SchoolSupplyRepository(_dbContext, _logger);

            Weeks = new WeekRepository(_dbContext, _logger);

            SchoolYears = new SchoolYearRepository(_dbContext, _logger);

            Books = new BookRepository(_dbContext, _logger);

            Periods = new PeriodRepository(_dbContext, _logger);

            PeriodDetails = new PeriodDetailRepository(_dbContext, _logger);

            Prompts = new PromptRepository(_dbContext, _logger);

            Quizzes = new QuizRepository(_dbContext, _logger);

            QuizQuestions = new QuizQuestionRepository(_dbContext, _logger);

            QuizAnswers = new QuizAnswerRepository(_dbContext, _logger);
        }

        public async Task CompleteAsync() => await _dbContext.SaveChangesAsync();

        public void Complete() => _dbContext.SaveChanges();
    }
}
