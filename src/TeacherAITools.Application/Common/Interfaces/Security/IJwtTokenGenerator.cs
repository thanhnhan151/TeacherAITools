using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Application.Common.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
