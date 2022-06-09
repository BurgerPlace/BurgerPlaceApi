using BurgerPlace.Models.Database;
using System;
using System.Collections.Generic;

namespace BurgerPlace.Models
{
    /// <summary>
    /// Table for storing users
    /// </summary>
    public partial class User
    {
        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public uint? RestaurantId { get; set; }
        public bool IsRoot { get; set; }
        public DateTime LastLogin { get; set; }

        public virtual Restaurant? Restaurant { get; set; }
    }
}
