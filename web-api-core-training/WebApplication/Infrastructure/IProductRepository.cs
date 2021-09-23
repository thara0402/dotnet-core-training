using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Infrastructure.Models;

namespace WebApplication.Infrastructure
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAsync();

        Task<Product> GetByIdAsync(long? id);

        Task InsertAsync(Product product);

        Task UpdateAsync();

        Task DeleteAsync(Product product);
    }
}
