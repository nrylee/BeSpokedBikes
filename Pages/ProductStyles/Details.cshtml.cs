using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.ProductStyles
{
    public class DetailsModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public DetailsModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductStyle ProductStyle { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductStyle = await _context.ProductStyles
                .Include(p => p.Product).FirstOrDefaultAsync(m => m.Id == id);

            if (ProductStyle == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
