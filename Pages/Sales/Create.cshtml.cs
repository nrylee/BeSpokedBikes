using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.Sales
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
        ViewData["Customer_Id"] = new SelectList(_context.Customers, "Id", "Id");
        ViewData["SalesPerson_Id"] = new SelectList(_context.SalesPeople, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Sale Sale { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Sales.Add(Sale);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
