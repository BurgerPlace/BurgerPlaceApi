namespace BurgerPlace.Models.Request
{
    public class UpdateIngredient
    {
        [Required]
        [MaxLength(32)]
        public string name { get; set; } = null!;
        [Required]
        public decimal price { get; set; }
    }
}
