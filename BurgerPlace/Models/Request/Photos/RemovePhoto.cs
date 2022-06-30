namespace BurgerPlace.Models.Request.Photos
{
    public class RemovePhoto
    {
        [Required]
        public string Path { get; set; } = null!;
    }
}
