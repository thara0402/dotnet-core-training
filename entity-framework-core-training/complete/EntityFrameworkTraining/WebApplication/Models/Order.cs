using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public long OrderId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UserId { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
