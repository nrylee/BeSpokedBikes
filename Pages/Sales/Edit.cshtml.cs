using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.Sales
{
    public class EditModel : PageModel
    {
        private readonly BeSpokedBikes.Data.ApplicationDbContext _context;

        public EditModel(BeSpokedBikes.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sale Sale { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sale = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.SalesPerson)
                .Include(s => s.Items).ThenInclude(si => si.ProductStyle).ThenInclude(ps => ps.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Sale == null)
            {
                return NotFound();
            }
           ViewData["Customer_Id"] = new SelectList(_context.Customers, "Id", "FullName");
           ViewData["SalesPerson_Id"] = new SelectList(_context.SalesPeople, "Id", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(Sale.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            if (Sale.SellDateTime.HasValue)
            {
                var sale = await _context.Sales.Include(s => s.Items).ThenInclude(i => i.ProductStyle).ThenInclude(i => i.Product).ThenInclude(i => i.Discounts).SingleAsync(s => s.Id==Sale.Id);
                foreach (var item in sale.Items)
                {
                    item.PreDiscountPricePer = item.ProductStyle.Product.SalesPrice;
                    item.ProductStyle.QuantityOnHand -= item.Quantity;
                    foreach (var availableDiscount in item.ProductStyle.Product.Discounts.Where(d => d.BeginDate<=sale.SellDateTime && d.EndDate >= sale.SellDateTime))
                    {
                        var appliedDiscount = new SaleItemDiscount
                        {
                            AmountDiscounted = availableDiscount.PercentageDiscounted * item.PreDiscountPricePer,
                            Discount_Id = availableDiscount.Id,
                            SaleItem_Id = item.Id
                        };
                        _context.SaleItemDiscounts.Add(appliedDiscount);
                        item.AppliedDiscounts.Add(appliedDiscount);
                    }
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(Sale.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
