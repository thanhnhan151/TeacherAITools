namespace TeacherAITools.Application.Authentication.Common
{
    public class AuthenticationResult
    {
        public string AccessToken { get; private set; }

        public string RefreshToken { get; private set; }


        public AuthenticationResult(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
