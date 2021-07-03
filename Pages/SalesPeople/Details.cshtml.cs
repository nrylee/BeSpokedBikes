using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.SalesPeople
{
    public class DetailsModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public DetailsModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public SalesPerson SalesPerson { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SalesPerson = await _context.SalesPeople.FirstOrDefaultAsync(m => m.Id == id);

            if (SalesPerson == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
