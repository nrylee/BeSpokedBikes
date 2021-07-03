using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.AppliedDiscounts
{
    public class EditModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public EditModel(BeSpokedBikes.Data.ApplicationDbContext context)
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
           ViewData["Discount_Id"] = new SelectList(_context.Discounts, "Id", "Id");
           ViewData["SaleItem_Id"] = new SelectList(_context.SaleItems, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SaleItemDiscount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleItemDiscountExists(SaleItemDiscount.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SaleItemDiscountExists(int id)
        {
            return _context.SaleItemDiscounts.Any(e => e.Id == id);
        }
    }
}
