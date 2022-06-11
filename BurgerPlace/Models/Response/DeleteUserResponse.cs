namespace BurgerPlace.Models.Response
{
    public class DeleteUserResponse
    {
        public DeleteUserResponse(string _username)
        {
            username = _username;
        }
        public string message { get; set; } = "Successfully removed user from system";
        public string username { get; set; }
    }
}
