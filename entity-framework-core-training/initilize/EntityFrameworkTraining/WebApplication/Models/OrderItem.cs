﻿using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public byte[] Ts { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
