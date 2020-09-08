using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<IList<Order>> GetAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<OrderItem>> GetItemsAsync(long? id)
        {
            throw new NotImplementedException();
        }
    }
}
