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
    public class IndexModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public IndexModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SaleItemDiscount> SaleItemDiscount { get;set; }

        public async Task OnGetAsync()
        {
            SaleItemDiscount = await _context.SaleItemDiscounts
                .Include(s => s.Discount)
                .Include(s => s.SaleItem).ToListAsync();
        }
    }
}
