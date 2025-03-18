namespace TeacherAITools.Application.Common.Interfaces.Persistence.Base
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IModuleRepository Modules { get; }
        ICurriculumRepository Curriculums { get; }

        Task CompleteAsync();
    }
}
