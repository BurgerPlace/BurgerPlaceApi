namespace BurgerPlace.Models.Request.Ingredients
{
    public class CreateCategory
    {
        [Required]
        [MaxLength(24)]
        public string Name { get; set; } = null!;
    }
}
