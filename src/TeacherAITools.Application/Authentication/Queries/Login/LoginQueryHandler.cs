using MediatR;
using Microsoft.EntityFrameworkCore;
using TeacherAITools.Application.Authentication.Common;
using TeacherAITools.Application.Common.Enums;
using TeacherAITools.Application.Common.Exceptions;
using TeacherAITools.Application.Common.Extensions;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Application.Common.Interfaces.Security;
using TeacherAITools.Domain.Wrappers;

namespace TeacherAITools.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork) : IRequestHandler<LoginQuery, Response<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var userQuery = await _unitOfWork.Users.GetAsync(
                expression: user => user.Username.ToLower().Equals(request.Username.ToLower())
                                    && user.PasswordHash != null
                                    && user.PasswordHash.Equals(request.Password),
                includeFunc: user => user.Include(u => u.Role));

            var user = userQuery.FirstOrDefault() ?? throw new ApiException(ResponseCode.INVALID_CREDENTIALS);

            if (!user.IsActive) throw new ApiException(ResponseCode.INACTIVE_USER);

            var token = _jwtTokenGenerator.GenerateJwtToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateJwtRefreshToken(user);

            return new Response<AuthenticationResult>(code: (int)ResponseCode.SUCCESS, data: new AuthenticationResult(token, refreshToken), message: ResponseCode.SUCCESS.GetDescription());
        }
    }
}
