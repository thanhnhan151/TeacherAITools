using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TeacherAITools.Application.Authentication.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Security;
using TeacherAITools.Application.Common.Interfaces.Services;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Authentication.Queries.RequestToken
{
    public class RefreshTokenQueryHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        IJwtTokenGenerator jwtGenerator) : IRequestHandler<RefreshTokenQuery, Response<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IJwtTokenGenerator _jwtGenerator = jwtGenerator;

        public async Task<Response<AuthenticationResult>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var principal = _currentUserService.GetCurrentPrincipalFromToken(request.RefreshToken);

            var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? throw new ApiException(ResponseCode.AUTH_ERR_REFRESH_TOKEN);

            var userQuery = await _unitOfWork.Users.GetAsync(
                user => user.Email.ToLower().Equals(email.ToLower())
            );

            var user = userQuery.Include(user => user.Role).FirstOrDefault() ?? throw new ApiException(ResponseCode.AUTH_ERR_REFRESH_TOKEN);

            var newAccessToken = _jwtGenerator.GenerateJwtToken(user);
            var newRefreshToken = _jwtGenerator.GenerateJwtRefreshToken(user);

            return new Response<AuthenticationResult>(code: (int)ResponseCode.SUCCESS, data: new AuthenticationResult(newAccessToken, newRefreshToken), message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
