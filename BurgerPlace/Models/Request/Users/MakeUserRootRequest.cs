namespace BurgerPlace.Models.Request.Users
{
    public class MakeUserRootRequest
    {
        [Required]
        public string username { get; set; } = null!;
    }
}
