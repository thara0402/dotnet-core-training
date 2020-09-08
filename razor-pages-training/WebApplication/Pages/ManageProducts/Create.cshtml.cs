using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Infrastructure;
using WebApplication.Models;

namespace WebApplication.Pages.ManageProducts
{
    // Labo #12 ---------------------------- ↓
    [Authorize(Roles = "administrators")]
    // Labo #12 ---------------------------- ↑
    public class CreateModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        [BindProperty]
        public Product Product { get; set; }

        public CreateModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyProduct = new Product();

            if (await TryUpdateModelAsync(
                emptyProduct,
                nameof(Product),
                p => p.Desc, p => p.Name, p => p.Price))
            {
                await _productRepository.InsertAsync(Product);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}