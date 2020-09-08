using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Infrastructure;
using WebApplication.Models;

namespace WebApplication.Pages.Orders
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public IList<Order> Orders { get; set; }

        public IndexModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.Identity.Name;
            if (userId == null)
            {
                return BadRequest();
            }
            Orders = await _orderRepository.GetAsync(userId);
            return Page();
        }
    }
}