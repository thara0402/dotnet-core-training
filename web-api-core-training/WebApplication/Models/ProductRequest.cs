﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ProductRequest
    {
        /// <summary>
        /// 説明
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Desc { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 価格
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
