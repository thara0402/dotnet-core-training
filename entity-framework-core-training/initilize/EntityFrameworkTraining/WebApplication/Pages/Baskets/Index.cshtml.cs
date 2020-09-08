using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Infrastructure;
using WebApplication.Models;

namespace WebApplication.Pages.Baskets
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IBasketRepository _basketRepository;
		private readonly IOrderRepository _orderRepository;

		public IList<BasketItem> BasketItems { get; set; }

        public IndexModel(IBasketRepository basketRepository, IOrderRepository orderRepository)
        {
            _basketRepository = basketRepository;
			_orderRepository = orderRepository;
		}

		public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.Identity.Name;
            if (userId == null)
            {
                return BadRequest();
            }
            BasketItems = await _basketRepository.GetItemsAsync(userId);
            return Page();
        }

		public async Task<IActionResult> OnPostAsync()
		{
			var userId = User.Identity.Name;
			if (userId == null)
			{
				return BadRequest();
			}

			await _orderRepository.InsertAsync(userId);
			return RedirectToPage("../Orders/Index");
		}
	}
}