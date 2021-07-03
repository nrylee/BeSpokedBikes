using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.SaleItems
{
    public class DetailsModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public DetailsModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
