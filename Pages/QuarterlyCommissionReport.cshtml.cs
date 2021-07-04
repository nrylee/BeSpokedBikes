using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.Pages
{
    public class QuarterlyCommissionReportModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public QuarterlyCommissionReportModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Dictionary<int, IEnumerable<Data.Sale>> SalesByQuarter { get; set; }
        public int Year { get; set; }

        public void OnGet(int? year)
        {
            if (!year.HasValue)
            {
                Year = DateTime.Now.Year;
            }
            else Year = year.Value;
            var q1Start = new DateTime(Year, 1, 1);
            var q2Start = new DateTime(Year, 4, 1);
            var q3Start = new DateTime(Year, 7, 1);
            var q4Start = new DateTime(Year, 10, 1);
            var endOfYear = new DateTime(Year, 12, 31);

            SalesByQuarter = new Dictionary<int, IEnumerable<Data.Sale>>();

            var includableSales = _context.Sales.Include(s => s.SalesPerson).Include(s => s.Items).ThenInclude(i => i.AppliedDiscounts).Include(s => s.Items).ThenInclude(i => i.ProductStyle).ThenInclude(ps => ps.Product);
            SalesByQuarter.Add(1, includableSales.Where(sale => sale.SellDateTime.HasValue && sale.SellDateTime >= q1Start && sale.SellDateTime < q2Start).AsEnumerable());
            SalesByQuarter.Add(2, includableSales.Where(sale => sale.SellDateTime.HasValue && sale.SellDateTime >= q2Start && sale.SellDateTime < q3Start).AsEnumerable());
            SalesByQuarter.Add(3, includableSales.Where(sale => sale.SellDateTime.HasValue && sale.SellDateTime >= q3Start && sale.SellDateTime < q4Start).AsEnumerable());
            SalesByQuarter.Add(4, includableSales.Where(sale => sale.SellDateTime.HasValue && sale.SellDateTime >= q4Start && sale.SellDateTime <= endOfYear).AsEnumerable());
        }
    }
}
