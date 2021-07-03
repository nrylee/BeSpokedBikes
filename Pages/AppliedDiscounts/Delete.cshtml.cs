using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.AppliedDiscounts
{
    public class DeleteModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public DeleteModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SaleItemDiscount SaleItemDiscount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SaleItemDiscount = await _context.SaleItemDiscounts
                .Include(s => s.Discount)
                .Include(s => s.SaleItem).FirstOrDefaultAsync(m => m.Id == id);

            if (SaleItemDiscount == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SaleItemDiscount = await _context.SaleItemDiscounts.FindAsync(id);

            if (SaleItemDiscount != null)
            {
                _context.SaleItemDiscounts.Remove(SaleItemDiscount);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
