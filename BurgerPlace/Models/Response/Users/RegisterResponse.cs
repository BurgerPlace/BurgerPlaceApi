namespace BurgerPlace.Models.Response.Users
{
    public class RegisterResponse
    {
        public class DuplicatedUsernameOrEmailResponse
        {
            public string message { get; set; } = "User with this username or email already exists";
        }

        public class SuccessfullyCreatedNewUserResponse
        {
            public string message { get; set; } = "Successfully created new user account";
        }
    }
}
