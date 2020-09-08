using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public interface IOrderRepository
    {
        Task<IList<Order>> GetAsync(string userId);

        Task InsertAsync(string userId);

        Task<IList<OrderItem>> GetItemsAsync(long? id);
    }
}
