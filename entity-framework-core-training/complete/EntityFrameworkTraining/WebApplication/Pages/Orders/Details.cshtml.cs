using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Infrastructure;
using WebApplication.Models;

namespace WebApplication.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public IList<OrderItem> OrderItems { get; set; }

        public DetailsModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            OrderItems = await _orderRepository.GetItemsAsync(id);
            if (OrderItems == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}