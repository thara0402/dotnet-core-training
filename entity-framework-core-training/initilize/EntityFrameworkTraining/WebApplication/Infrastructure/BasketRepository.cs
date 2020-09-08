using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public class BasketRepository : IBasketRepository
    {
        public async Task<IList<BasketItem>> GetItemsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task InsertItemAsync(string userId, Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<BasketItem> GetItemByIdAsync(long? id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteItemAsync(string userId, long? id)
        {
            throw new NotImplementedException();
        }
    }
}
