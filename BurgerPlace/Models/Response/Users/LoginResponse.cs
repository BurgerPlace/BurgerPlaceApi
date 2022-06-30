namespace BurgerPlace.Models.Response.Users
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
            public string token { get; set; } = null!;
        }
        public class LoginWrongData
        {
            public string message { get; set; } = "Wrong username and/or password";
        }
    }
}
