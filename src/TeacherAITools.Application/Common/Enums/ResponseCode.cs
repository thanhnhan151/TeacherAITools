using System.ComponentModel;

namespace TeacherAITools.Application.Common.Enums
{
    public enum ResponseCode
    {
        #region Common

        [Description("Success")]
        SUCCESS = 0,
        [Description("Failed")]
        FAILED = 1,
        [Description("Common Error")]
        COMMON_ERR = 2,
        [Description("Invalid param")]
        INVALID_PARAM = 3,
        [Description("Invalid session")]
        INVALID_SESSION = 4,
        [Description("Unhandled request")]
        UNHANDLED_REQUEST = 5,
        [Description("Error when calling third party")]
        THIRD_PARTY_ERR = 6,
        [Description("Error when processing JSON")]
        JSON_PROCESS_ERR = 7,
        [Description("Invalid response code")]
        INVALID_RESPONSE_CODE = 8,
        [Description("Key is conflict")]
        CONFLICT = 9,

        #endregion

        #region Authen && Author

        [Description("Invalid username or password")]
        INVALID_CREDENTIALS = 10,
        [Description("Error occurs when login with Google")]
        GOOGLE_AUTH_ERR = 11,
        [Description("Invalid refresh token")]
        INVALID_REFRESH_TOKEN = 12,
        [Description("Authentication failed")]
        FAILED_AUTHENTICATION = 13,
        [Description("Authentication failed: Outside email")]
        AUTH_ERR_OUT_EMAIL = 14,
        [Description("Invalid code")]
        INVALID_CODE = 15,
        [Description("Validation Error")] VALIDATION_ERR = 16,
        [Description("Unauthorized")] UNAUTHORIZED = 17,
        [Description("Google Id Token is invalid")] AUTH_ERR_GOOGLE_TOKEN = 18,
        [Description("Refresh Token is invalid")] AUTH_ERR_REFRESH_TOKEN = 19,

        #endregion

        #region User
        [Description("Username or Email has already been registered")]
        USERNAME_EMAIL_ERR = 20,
        [Description("Created successfully!")]
        CREATED_SUCCESS = 21,
        [Description("Updated successfully!")]
        UPDATED_SUCCESS = 22,
        [Description("Disabled successfully!")]
        DISABLED_SUCCESS = 23,
        [Description("Created unsuccessfully!")]
        CREATED_UNSUCC = 24,
        [Description("Updated unsuccessfully!")]
        UPDATED_UNSUCC = 25,
        [Description("Disabled unsuccessfully!")]
        DISABLED_UNSUCC = 26,
        [Description("User not found!")]
        USER_NOT_FOUND = 27,
        [Description("Your account is inactive! Please contact Admin for more info")]
        INACTIVE_USER = 28,
        #endregion

        #region Module
        [Description("Module is already exists.")]
        MODULE_ALREADY_EXISTS = 29,
        [Description("Module not found!")]
        MODULE_NOT_FOUND = 30,
        [Description("Deleted successfully!")]
        DELETED_SUCCESS = 31,
        #endregion

        #region Curriculum
        [Description("Curriculum not found!")]
        CURRICULUM_NOT_FOUND = 32,
        #endregion

        #region School
        [Description("School not found!")]
        SCHOOL_NOT_FOUND = 33,
        [Description("School's name has already exist!")]
        SCHOOL_NAME_ERR = 34
        #endregion
    }
}
