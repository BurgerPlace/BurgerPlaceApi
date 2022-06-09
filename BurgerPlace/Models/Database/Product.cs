using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    /// <summary>
    /// Table that will store products and all informations connected with it
    /// </summary>
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
            ProductCategories = new HashSet<ProductCategory>();
            ProductIngredients = new HashSet<ProductIngredient>();
            SetProducts = new HashSet<SetProduct>();
        }

        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public uint? PhotoId { get; set; }
        public bool Available { get; set; }
        public decimal Price { get; set; }

        public virtual Photo? Photo { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
        public virtual ICollection<SetProduct> SetProducts { get; set; }
    }
}
