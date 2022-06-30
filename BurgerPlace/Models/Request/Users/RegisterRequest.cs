namespace BurgerPlace.Models.Request.Users
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(32)]
        [MinLength(3)]
        public string name { get; set; } = string.Empty;
        [Required]
        [MaxLength(32)]
        [MinLength(3)]
        public string surname { get; set; } = string.Empty;
        [Required]
        [MaxLength(32)]
        [MinLength(5)]
        public string username { get; set; } = string.Empty;
        [Required]
        [MaxLength(32)]
        [MinLength(8)]
        public string password { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(32)]
        [MinLength(8)]
        public string email { get; set; } = string.Empty;
    }
}
