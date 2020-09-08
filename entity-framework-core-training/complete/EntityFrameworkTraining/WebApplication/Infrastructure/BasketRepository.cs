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
        private readonly ShopContext _context;

        public BasketRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<IList<BasketItem>> GetItemsAsync(string userId)
        {
            var basket = await _context.Basket.Where(basket => basket.UserId == userId)
                                .Include(basket => basket.BasketItem)
                                .ThenInclude(basketItem => basketItem.Product).FirstOrDefaultAsync();
            if (basket == null)
            {
                basket = new Basket()
                {
                    UserId = userId,
                    LastUpdate = DateTime.Now
                };
                _context.Basket.Add(basket);
                await _context.SaveChangesAsync();
            }
            return basket.BasketItem.ToList();
        }

        public async Task InsertItemAsync(string userId, Product product)
        {
            var basket = await _context.Basket.Where(basket => basket.UserId == userId).FirstOrDefaultAsync();
            if (basket == null)
            {
                basket = new Basket()
                {
                    UserId = userId,
                    LastUpdate = DateTime.Now
                };
                _context.Basket.Add(basket);
                await _context.SaveChangesAsync();
            }

            var basketItem = new BasketItem()
            {
                Basket = basket,
                Product = product,
                ProductId = product.ProductId,
                Quantity = 1,
                UnitPrice = product.Price
            };
            basket.BasketItem.Add(basketItem);
            await _context.SaveChangesAsync();
        }

        public async Task<BasketItem> GetItemByIdAsync(long? id)
        {
            return await _context.BasketItem.Where(basketItem => basketItem.BasketItemId == id)
                                            .Include(basketItem => basketItem.Product).FirstOrDefaultAsync();
        }

        public async Task DeleteItemAsync(string userId, long? id)
        {
            var basket = await _context.Basket.Where(basket => basket.UserId == userId)
                                              .Include(basket => basket.BasketItem).FirstOrDefaultAsync();
            if (basket == null)
            {
                return;
            }

            var basketItem = await _context.BasketItem.FindAsync(id);
            if (basketItem != null)
            {
                basket.BasketItem.Remove(basketItem);
                _context.BasketItem.Remove(basketItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
