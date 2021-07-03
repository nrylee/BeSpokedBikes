using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.Sales
{
    public class IndexModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public IndexModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Sale> Sale { get;set; }

        public async Task OnGetAsync()
        {
            Sale = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.SalesPerson).ToListAsync();
        }
    }
}
