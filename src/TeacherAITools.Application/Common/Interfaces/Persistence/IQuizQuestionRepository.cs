using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface IQuizQuestionRepository : IRepository<QuizQuestion>
    {
        int GetLastIdQuestion();
    }
}
