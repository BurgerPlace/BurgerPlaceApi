using System;
using System.Collections.Generic;

namespace BurgerPlace.Models.Database
{
    /// <summary>
    /// Table to store orders
    /// </summary>
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public uint Id { get; set; }
        public bool IsPickup { get; set; }
        public uint? AddressId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Phone { get; set; } = null!;
        public uint RestaurantId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual Address? Address { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
