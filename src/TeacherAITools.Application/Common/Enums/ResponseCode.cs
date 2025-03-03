using System.ComponentModel;

namespace TeacherAITools.Application.Common.Enums
{
    public enum ResponseCode
    {
        [Description("Success")]
        Success = 0,
        [Description("Failed")]
        Failed = 1,
        [Description("Common Error")]
        CommonError = 2,
        [Description("Invalid param")]
        InvalidParam = 3,
        [Description("Invalid session")]
        InvalidSession = 4,
        [Description("Unhandled request")]
        UnhandledRequest = 5,
        [Description("Error when calling third party")]
        ThirdPartyError = 6,
        [Description("Error when processing JSON")]
        JsonProcessingError = 7,
        [Description("Invalid response code")]
        ResponseCodeInvalid = 8,
        [Description("Key is conflict")]
        Conflict = 9,

        // Auth
        [Description("Invalid username or password")]
        InvalidUsernameOrPassword = 10,
        [Description("Error occurs when login with Google")]
        GoogleAuthError = 11,
        [Description("Invalid refresh token")]
        RefreshTokenInvalid = 12,
        [Description("Authentication failed")]
        AuthenticationFailed = 13,
        [Description("Authentication failed: Outside email")]
        AuthenticationFailedOutsideEmail = 14,
        [Description("Invalid code")]
        CodeInvalid = 15,

        [Description("Unauthorized")]
        UnauthorizedRequest = 11,
    }
}
