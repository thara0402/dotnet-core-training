using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        public async Task<IList<Product>> GetAsync()
        {
            return new List<Product>();
        }

        public async Task<Product> GetByIdAsync(long? id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
