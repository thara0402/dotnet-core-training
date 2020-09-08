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
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<IList<Product>> GetAsync()
        {
            return await _context.Product.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(long? id)
        {
            return await _context.Product.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task InsertAsync(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
