using System.Security.Claims;

namespace TeacherAITools.Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        public string? CurrentPrincipal { get; }

        public ClaimsPrincipal GetCurrentPrincipalFromToken(string token);
    }
}
