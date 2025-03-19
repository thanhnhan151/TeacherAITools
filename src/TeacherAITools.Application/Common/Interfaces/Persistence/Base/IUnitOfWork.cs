namespace TeacherAITools.Application.Common.Interfaces.Persistence.Base
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        IModuleRepository Modules { get; }

        ICurriculumRepository Curriculums { get; }

        ISchoolRepository Schools { get; }

        ICityRepository Cities { get; }

        IDistrictRepository Districts { get; }

        Task CompleteAsync();
    }
}
