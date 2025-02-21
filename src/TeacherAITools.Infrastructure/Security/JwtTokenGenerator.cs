using Microsoft.Extensions.Options;
using TeacherAITools.Application.Common.Interfaces.Security;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Infrastructure.Security
{
    public class JwtTokenGenerator(
        IDateTimeProvider dateTimeProvider,
        IOptions<JwtSettings> jwtOptions) : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

        public string GenerateToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
