using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public partial class Product
    {
        public Product()
        {
            BasketItem = new HashSet<BasketItem>();
            OrderItem = new HashSet<OrderItem>();
        }

        public long ProductId { get; set; }

        [Required]
        [StringLength(256)]
        public string Desc { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public byte[] Ts { get; set; }

        public virtual ICollection<BasketItem> BasketItem { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
