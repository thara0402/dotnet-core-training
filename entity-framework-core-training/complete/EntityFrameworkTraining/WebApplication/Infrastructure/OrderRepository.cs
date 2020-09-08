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
        private readonly ShopContext _context;

        public OrderRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<IList<Order>> GetAsync(string userId)
        {
            return await _context.Order.Where(order => order.UserId == userId)
                                       .OrderByDescending(order => order.LastUpdate).ToListAsync();
        }

        public async Task InsertAsync(string userId)
        {
            // バスケットの商品を注文テーブルに登録します。
            var basket = await _context.Basket.Where(basket => basket.UserId == userId)
                                              .Include(basket => basket.BasketItem).FirstOrDefaultAsync();
            if (basket == null)
            {
                throw new Exception("Basket not found.");
            }
            var order = new Order()
            {
                UserId = basket.UserId,
                LastUpdate = DateTime.Now
            };
            _context.Order.Add(order);

            // 登録したバスケットの商品をバスケットから削除します。
            foreach (var basketItem in basket.BasketItem.ToList())
            {
                var orderItem = new OrderItem()
                {
                    Order = order,
                    OrderId = order.OrderId,
                    Product = basketItem.Product,
                    ProductId = basketItem.ProductId,
                    Quantity = basketItem.Quantity,
                    UnitPrice = basketItem.UnitPrice
                };
                order.OrderItem.Add(orderItem);
                _context.BasketItem.Remove(basketItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IList<OrderItem>> GetItemsAsync(long? id)
        {
            var order = await _context.Order.Where(order => order.OrderId == id)
                                            .Include(order => order.OrderItem)
                                            .ThenInclude(orderItem => orderItem.Product).FirstOrDefaultAsync();
            if (order == null)
            {
                return null;
            }
            return order.OrderItem.ToList();
        }
    }
}
