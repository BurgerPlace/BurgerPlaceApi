using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    /// <summary>
    /// Table for storing categories
    /// </summary>
    public partial class Category
    {
        public Category()
        {
            ProductCategories = new HashSet<ProductCategory>();
            SetCategories = new HashSet<SetCategory>();
        }

        public uint Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<SetCategory> SetCategories { get; set; }
    }
}
