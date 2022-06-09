﻿using System;
using System.Collections.Generic;

namespace BurgerPlace.Models
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

        public virtual Address? AddressNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
