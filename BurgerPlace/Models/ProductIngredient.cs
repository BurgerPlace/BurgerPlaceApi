using System;
using System.Collections.Generic;

namespace BurgerPlace.Models
{
    /// <summary>
    /// Table for storing ingredients in products
    /// </summary>
    public partial class ProductIngredient
    {
        public uint Id { get; set; }
        public uint? ProductId { get; set; }
        public uint? IngredientId { get; set; }

        public virtual Ingredient? Ingredient { get; set; }
        public virtual Product? Product { get; set; }
    }
}
