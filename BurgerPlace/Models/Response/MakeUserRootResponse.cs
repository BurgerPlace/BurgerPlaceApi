namespace BurgerPlace.Models.Response
{
    public class MakeUserRootResponse
    {
        public MakeUserRootResponse(string _username)
        {
            username = _username;
        }
        public string message { get; set; } = "Successfully rised privileges of user to root";
        public string username { get; set; }
    }
}
