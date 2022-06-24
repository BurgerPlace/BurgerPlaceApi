using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
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
        public string Email { get; set; } = null!;

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Restaurant? Restaurant { get; set; }
    }
}
