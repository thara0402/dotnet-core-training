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
    public class DetailsModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public Product Product { get; set; }

        public DetailsModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _productRepository.GetByIdAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}