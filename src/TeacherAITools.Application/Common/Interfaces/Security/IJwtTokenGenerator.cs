using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Application.Common.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(User user);
        string GenerateJwtRefreshToken(User user);
    }
}
