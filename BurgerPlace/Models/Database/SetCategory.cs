using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    /// <summary>
    /// Table to store categories of set
    /// </summary>
    public partial class SetCategory
    {
        public uint Id { get; set; }
        public uint? SetId { get; set; }
        public uint? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Set? Set { get; set; }
    }
}
