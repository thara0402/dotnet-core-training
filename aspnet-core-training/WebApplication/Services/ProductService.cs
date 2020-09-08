using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Infrastructure;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public IList<Product> Get()
        {
            // Labo #8 ---------------------------- ↓
            //throw new Exception("Dummy Exception.");
            // Labo #8 ---------------------------- ↑

            var result = _repo.Get();
            if (result == null)
            {
                return new List<Product>();
            }
            return result;
        }
    }
}
