﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ProductResponse
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ProductId { get; set; }
        
        /// <summary>
        /// 説明
        /// </summary>
        public string Desc { get; set; }
        
        /// <summary>
        /// 商品名
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 価格
        /// </summary>
        public decimal Price { get; set; }
    }
}
