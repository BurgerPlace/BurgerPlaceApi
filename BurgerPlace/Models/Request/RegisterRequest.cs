
namespace BurgerPlace.Models.Request
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(32)]
        public string name { get; set; }
        [Required]
        [MaxLength(32)]
        public string surname { get; set; }
        [Required]
        [MaxLength(32)]
        public string username { get; set; }
        [Required]
        [MaxLength(32)]
        public string password { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(32)]
        public string email { get; set; }
    }
}
