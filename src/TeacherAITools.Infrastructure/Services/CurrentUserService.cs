using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Infrastructure.Security;

namespace TeacherAITools.Infrastructure.Services
{
    public class CurrentUserService(
        IHttpContextAccessor accessor,
        IUnitOfWork unitOfWork,
        IOptions<JwtSettings> jwtOptions) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;

        public string? CurrentPrincipal
        {
            get
            {
                var identity = _accessor?.HttpContext?.User.Identity as ClaimsIdentity;
                if (identity == null || !identity.IsAuthenticated) return null;

                var claims = identity.Claims;

                var userId = claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value ?? null;

                return userId;
            }
        }

        public ClaimsPrincipal GetCurrentPrincipalFromToken(string token)
        {
            var tokenValidationParams = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ApiException(ResponseCode.AUTH_ERR_REFRESH_TOKEN);
            }

            return principal;
        }
    }
}
