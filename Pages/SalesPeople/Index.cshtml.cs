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
    public class IndexModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public IndexModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SalesPerson> SalesPerson { get;set; }

        public async Task OnGetAsync()
        {
            SalesPerson = await _context.SalesPeople.ToListAsync();
        }
    }
}
