using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.Discounts
{
    public class DetailsModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public DetailsModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Discount Discount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Discount = await _context.Discounts
                .Include(d => d.Product).FirstOrDefaultAsync(m => m.Id == id);

            if (Discount == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
