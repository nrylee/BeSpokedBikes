using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.SaleItems
{
    public class EditModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public EditModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SaleItem SaleItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SaleItem = await _context.SaleItems
                .Include(s => s.ProductStyle)
                .Include(s => s.Sale).FirstOrDefaultAsync(m => m.Id == id);

            if (SaleItem == null)
            {
                return NotFound();
            }
           ViewData["ProductStyle_Id"] = new SelectList(_context.ProductStyles, "Id", "Id");
           ViewData["Sale_Id"] = new SelectList(_context.Sales, "Id", "Id");
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

            _context.Attach(SaleItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleItemExists(SaleItem.Id))
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

        private bool SaleItemExists(int id)
        {
            return _context.SaleItems.Any(e => e.Id == id);
        }
    }
}
