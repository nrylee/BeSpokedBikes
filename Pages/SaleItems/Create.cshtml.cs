using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeSpokedBikes.Data;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.Pages.SaleItems
{
    public class CreateModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public CreateModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ProductStyle_Id"] = new SelectList(_context.ProductStyles, "Id", "StyleName");
            if (Request.Query.ContainsKey("saleid") && int.TryParse(Request.Query["saleid"], out int saleid))
                ViewData["Sale_Id"] = new SelectList(_context.Sales, "Id", "Id", saleid);
            else
                ViewData["Sale_Id"] = new SelectList(_context.Sales, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public SaleItem SaleItem { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SaleItems.Add(SaleItem);
            
            SaleItem.PreDiscountPricePer = (await _context.ProductStyles.Include(ps => ps.Product).FirstAsync(ps => ps.Id == SaleItem.ProductStyle_Id)).Product.SalesPrice;
            foreach (var discount in (await _context.SaleItems.Include(si => si.ProductStyle).ThenInclude(ps => ps.Product).ThenInclude(p => p.Discounts).FirstOrDefaultAsync(si => si.Id == SaleItem.Id))?.ProductStyle?.Product?.Discounts?.Where(discount => discount.BeginDate <= DateTime.Now && discount.EndDate <= DateTime.Now) ?? new List<Discount>())
            {
                _context.SaleItemDiscounts.Add(new SaleItemDiscount
                {
                    AmountDiscounted = discount.PercentageDiscounted * SaleItem.PreDiscountPricePer,
                    Discount_Id = discount.Id,
                    SaleItem_Id = SaleItem.Id
                });
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("../Sales/Edit", new { Id = SaleItem.Sale_Id });
        }
    }
}
