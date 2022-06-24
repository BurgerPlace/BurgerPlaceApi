using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    /// <summary>
    /// Table for storing ingredients in products
    /// </summary>
    public partial class ProductIngredient
    {
        public uint Id { get; set; }
        public uint? ProductId { get; set; }
        public uint? IngredientId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Ingredient? Ingredient { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Product? Product { get; set; }
    }
}
