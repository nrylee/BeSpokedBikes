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
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public async Task OnGetAsync(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            var queryable = _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.SalesPerson)
                .Include(s => s.Items)
                .ThenInclude(i => i.AppliedDiscounts)
                .Include(s => s.Items)
                .ThenInclude(i => i.ProductStyle)
                .ThenInclude(p => p.Product).AsQueryable();
            if (StartDate.HasValue || EndDate.HasValue)
            {
                queryable = queryable.Where(s => s.SellDateTime.HasValue);
                if (StartDate.HasValue)
                {
                    queryable = queryable.Where(s => s.SellDateTime >= StartDate);
                }
                if (EndDate.HasValue)
                {
                    queryable = queryable.Where(s => s.SellDateTime <= EndDate);
                }
            }
            Sale = await queryable
                .ToListAsync();
        }
    }
}
