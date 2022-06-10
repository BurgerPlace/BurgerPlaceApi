namespace BurgerPlace.Models.Request
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(32)]
        [MinLength(5)]
        public string username { get; set; }
        [Required]
        [MaxLength(32)]
        [MinLength(8)]
        public string password { get; set; }
    }
}
