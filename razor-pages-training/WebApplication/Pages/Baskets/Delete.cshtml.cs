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
	public class DeleteModel : PageModel
    {
        private readonly IBasketRepository _basketRepository;

        [BindProperty]
        public BasketItem BasketItem { get; set; }

        public DeleteModel(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

		public async Task<IActionResult> OnGetAsync(long? id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			BasketItem = await _basketRepository.GetItemByIdAsync(id);
			if (BasketItem == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(long? id)
		{
			var userId = User.Identity.Name;
			if (userId == null)
			{
				return BadRequest();
			}

			await _basketRepository.DeleteItemAsync(userId, id);
			return RedirectToPage("./Index");
		}
	}
}