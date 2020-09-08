using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Infrastructure;
using WebApplication.Models;

namespace WebApplication.Pages.Products
{
	public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;
		private readonly IBasketRepository _basketRepository;

		public IList<Product> Products { get; set; }

        public IndexModel(IProductRepository productRepository, IBasketRepository basketRepository)
        {
            _productRepository = productRepository;
			_basketRepository = basketRepository;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			Products = await _productRepository.GetAsync();
			return Page();
		}

		public async Task<IActionResult> OnGetAddBasketAsync(long? id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			var product = await _productRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			var userId = User.Identity.Name;
			if (string.IsNullOrEmpty(userId))
			{
				return BadRequest();
			}

			await _basketRepository.InsertItemAsync(userId, product);

			return RedirectToPage("../Baskets/Index");
		}
	}
}