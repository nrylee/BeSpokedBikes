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
    public class IndexModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public IndexModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SaleItem> SaleItem { get;set; }

        public async Task OnGetAsync()
        {
            SaleItem = await _context.SaleItems
                .Include(s => s.ProductStyle)
                .Include(s => s.Sale).ToListAsync();
        }
    }
}
