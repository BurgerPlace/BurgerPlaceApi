namespace BurgerPlace.Models.Request
{
    public class DeleteUser
    {
        [Required]
        public string username { get; set; } = null!;
    }
}
