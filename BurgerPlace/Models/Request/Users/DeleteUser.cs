namespace BurgerPlace.Models.Request.Users
{
    public class DeleteUser
    {
        [Required]
        public string username { get; set; } = null!;
    }
}
