using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    /// <summary>
    /// Table for storing ingredients
    /// </summary>
    public partial class Ingredient
    {
        public Ingredient()
        {
            ProductIngredients = new HashSet<ProductIngredient>();
        }

        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}
