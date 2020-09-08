using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Infrastructure;
using WebApplication.Models;

namespace WebApplication.Pages.ManageProducts
{
    // Labo #12 ---------------------------- ↓
    [Authorize(Roles = "administrators")]
    // Labo #12 ---------------------------- ↑
    public class EditModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public string ErrorMessage { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        public EditModel(IProductRepository productRepository)
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                product,
                nameof(Product),
                p => p.Desc, p => p.Name, p => p.Price))
            {
                try
                {
                    await _productRepository.UpdateAsync();
                    return RedirectToPage("./Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ErrorMessage = "他のユーザーが同時に編集しています。再度、処理を実行してください。";
                }
            }
            return Page();
        }
    }
}