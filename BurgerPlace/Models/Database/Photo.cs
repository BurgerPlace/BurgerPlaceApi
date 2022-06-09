using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    /// <summary>
    /// Table for storing photos
    /// </summary>
    public partial class Photo
    {
        public Photo()
        {
            Products = new HashSet<Product>();
        }

        public uint Id { get; set; }
        public string Path { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
