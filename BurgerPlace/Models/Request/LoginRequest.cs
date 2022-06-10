namespace BurgerPlace.Models.Request
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(32)]
        public string username { get; set; }
        [Required]
        [MaxLength(32)]
        public string password { get; set; }
    }
}
