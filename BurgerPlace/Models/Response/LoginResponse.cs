namespace BurgerPlace.Models.Response
{
    public class LoginResponse
    {
        public class LoginSuccessResponse
        {
            public LoginSuccessResponse(string _token)
            {
                token = _token;
            }
            public string message { get; set; } = "Successfully logged in";
            public string token { get; set; }
        }
        public class LoginWrongPassword
        {
            public static string message { get; set; } = "Wrong password";
        }
        public class LoginWrongUsername
        {
            public static string message { get; set; } = "Wrong username";
        }
    }
}
