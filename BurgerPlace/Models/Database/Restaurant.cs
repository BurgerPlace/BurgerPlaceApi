using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    /// <summary>
    /// Table to store restaurants data
    /// </summary>
    public partial class Restaurant
    {
        public Restaurant()
        {
            Orders = new HashSet<Order>();
            Users = new HashSet<User>();
        }

        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public uint? Address { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Address? AddressNavigation { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
