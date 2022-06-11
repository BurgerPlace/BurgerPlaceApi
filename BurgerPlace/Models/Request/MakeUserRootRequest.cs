namespace BurgerPlace.Models.Request
{
    public class MakeUserRootRequest
    {
        [Required]
        public string username { get; set; } = string.Empty;
    }
}
