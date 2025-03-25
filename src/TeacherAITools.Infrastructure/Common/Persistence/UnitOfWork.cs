﻿using Microsoft.Extensions.Logging;
using TeacherAITools.Application.Common.Interfaces.Persistence;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Infrastructure.Blogs;
using TeacherAITools.Infrastructure.Citites;
using TeacherAITools.Infrastructure.Curriculums;
using TeacherAITools.Infrastructure.Lessons;
using TeacherAITools.Infrastructure.Modules;
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
        }

        public async Task CompleteAsync() => await _dbContext.SaveChangesAsync();
    }
}
