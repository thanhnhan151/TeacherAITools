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
        SCHOOL_NAME_ERR = 34,
        #endregion

        #region City
        [Description("City not found!")]
        CITY_NOT_FOUND = 35,
        #endregion

        #region District
        [Description("District not found!")]
        DISTRICT_NOT_FOUND = 36,
        #endregion

        #region Blog
        [Description("Blog not found!")]
        BLOG_NOT_FOUND = 37,
        #endregion

        #region Comment
        [Description("Comment not found!")]
        COMMENT_NOT_FOUND = 38,
        #endregion

        #region Lesson
        [Description("Lesson not found!")]
        LESSON_NOT_FOUND = 39,
        #endregion

        #region Photo
        [Description("Invalid file extension")]
        INVALID_FILE_EXTENSION = 40,
        [Description("Maximun size can be 5mb")]
        INVALID_FILE_SIZE = 41,
        #endregion

        [Description("Specified lesson type not exist in database.")]
        ID_LESSON_TYPE_DONT_EXIST = 42,

        [Description("Specified requirement not exist in database.")]
        ID_REQUIREMENT_DONT_EXIST = 43,

        [Description("Specified note not exist in database.")]
        ID_NOTE_DONT_EXIST = 44,

        [Description("Specified school supply not exist in database.")]
        ID_SCHOOL_SUPPLY_DONT_EXIST = 45,

        [Description("Specified week not exist in database.")]
        ID_WEEK_DONT_EXIST = 46,

        [Description("Specified school year not exist in database.")]
        ID_SCHOOL_YEAR_DONT_EXIST = 47,

        [Description("Specified grade not exist in database.")]
        ID_GRADE_DONT_EXIST = 48,

        [Description("Specified book not exist in database.")]
        ID_BOOK_DONT_EXIST = 49,

        #region Quiz
        [Description("Quiz not found!")]
        QUIZ_NOT_FOUND = 50,
        #endregion

        [Description("Subject specialist has already existed!")]
        MANAGER_HAS_EXISTED = 51,

        [Description("Vice manager has already existed!")]
        VICE_MANAGER_HAS_EXISTED = 52,

        [Description("Email does not exist")]
        EMAIL_NOT_FOUND = 53,

        [Description("Otp is incorrecct")]
        INCORRECT_RESET_OTP = 54,

        [Description("Confirmed password does not match!")]
        CONFIRM_PASSWORD_NOT_MATCH = 55,

        [Description("You have already generated content for this lesson!")]
        ALREADY_GENERATED_LESSON = 56,

        [Description("Lesson has already existed!")]
        ALREADY_EXISTED_LESSON = 57,

        [Description("Prompt not found!")]
        PROMPT_NOT_FOUND = 58,

        [Description("Lesson has already existed!")]
        TEACHER_LESSON_DONT_EXIST = 59,
    }
}
