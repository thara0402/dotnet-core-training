using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Basket
    {
        public Basket()
        {
            BasketItem = new HashSet<BasketItem>();
        }

        public long BasketId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UserId { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<BasketItem> BasketItem { get; set; }
    }
}
