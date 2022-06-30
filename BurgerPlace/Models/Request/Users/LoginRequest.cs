namespace BurgerPlace.Models.Request.Users
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(32)]
        [MinLength(5)]
        public string username { get; set; } = null!;
        [Required]
        [MaxLength(32)]
        [MinLength(8)]
        public string password { get; set; } = null!;
    }
}
