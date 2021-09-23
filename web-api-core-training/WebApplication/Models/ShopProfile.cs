using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Infrastructure.Models;

namespace WebApplication.Models
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();
        }
    }
}
