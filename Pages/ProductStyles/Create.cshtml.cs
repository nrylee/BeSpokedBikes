using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.ProductStyles
{
    public class CreateModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public CreateModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Product_Id"] = new SelectList(_context.Products, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public ProductStyle ProductStyle { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingStyle = _context.ProductStyles.FirstOrDefault(ps => ps.Product_Id == ProductStyle.Product_Id && ps.StyleName == ProductStyle.StyleName);
            if (existingStyle != null)
            {
                return Page();
            }

            _context.ProductStyles.Add(ProductStyle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
