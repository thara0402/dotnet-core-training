using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public interface IBasketRepository
    {
        Task<IList<BasketItem>> GetItemsAsync(string userId);

        Task InsertItemAsync(string userId, Product product);

        Task<BasketItem> GetItemByIdAsync(long? id);

        Task DeleteItemAsync(string userId, long? id);
    }
}
