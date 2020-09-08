﻿using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class BasketItem
    {
        public long BasketItemId { get; set; }
        public long BasketId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public byte[] Ts { get; set; }

        public virtual Basket Basket { get; set; }
        public virtual Product Product { get; set; }
    }
}
