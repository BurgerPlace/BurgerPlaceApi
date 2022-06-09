using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    /// <summary>
    /// Set of products like for ex. Happy Meal
    /// </summary>
    public partial class Set
    {
        public Set()
        {
            SetCategories = new HashSet<SetCategory>();
            SetProducts = new HashSet<SetProduct>();
        }

        public uint Id { get; set; }
        public bool Available { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<SetCategory> SetCategories { get; set; }
        public virtual ICollection<SetProduct> SetProducts { get; set; }
    }
}
