
namespace BurgerPlace.Models.Request
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(32)]
        [MinLength(3)]
        public string name { get; set; }
        [Required]
        [MaxLength(32)]
        [MinLength(3)]
        public string surname { get; set; }
        [Required]
        [MaxLength(32)]
        [MinLength(5)]
        public string username { get; set; }
        [Required]
        [MaxLength(32)]
        [MinLength(8)]
        public string password { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(32)]
        [MinLength(8)]
        public string email { get; set; }
    }
}
