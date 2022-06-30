namespace BurgerPlace.Models.Request.Ingredients
{
    public class CreateIngredient
    {
        [Required]
        [MaxLength(32)]
        public string name { get; set; } = null!;
        [Required]
        public decimal price { get; set; }
    }
}
