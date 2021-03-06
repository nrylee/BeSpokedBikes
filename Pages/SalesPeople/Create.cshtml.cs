using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeSpokedBikes.Data;

namespace BeSpokedBikes.Pages.SalesPeople
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
            return Page();
        }

        [BindProperty]
        public SalesPerson SalesPerson { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingSalesPerson = _context.SalesPeople.FirstOrDefault(sp => sp.FirstName == SalesPerson.FirstName && sp.LastName == SalesPerson.LastName && sp.Phone == SalesPerson.Phone);
            if (existingSalesPerson != null)
            {
                return Page();
            }

            _context.SalesPeople.Add(SalesPerson);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
