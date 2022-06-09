﻿using System;
using System.Collections.Generic;

namespace BurgerPlace.Models
{
    /// <summary>
    /// Table to store address informations
    /// </summary>
    public partial class Address
    {
        public Address()
        {
            Orders = new HashSet<Order>();
            Restaurants = new HashSet<Restaurant>();
        }

        public uint Id { get; set; }
        public string City { get; set; } = null!;
        public string? Street { get; set; }
        public string StreetNumber { get; set; } = null!;
        public string? FlatNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
