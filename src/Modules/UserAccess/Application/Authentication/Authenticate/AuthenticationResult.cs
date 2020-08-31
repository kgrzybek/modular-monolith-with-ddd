namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Authentication.Authenticate
{
    public class AuthenticationResult
    {
        public AuthenticationResult(string authenticationError)
        {
            IsAuthenticated = false;
            AuthenticationError = authenticationError;
        }

        public AuthenticationResult(UserDto user)
        {
            this.IsAuthenticated = true;
            this.User = user;
        }

        public bool IsAuthenticated { get; }

        public string AuthenticationError { get; }

        public UserDto User { get; }
    }
}