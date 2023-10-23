using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TugasAsinkron1.Data;
using TugasAsinkron1.Models;

namespace TugasAsinkron1.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly TugasAsinkron1.Data.TugasAsinkron1Context _context;

        public DeleteModel(TugasAsinkron1.Data.TugasAsinkron1Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProdukId == id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                Product = product;
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }
            TempData["ExportMessage"] = "Product data has been deleted successfully.";

            return RedirectToPage("./Index");
        }
    }
}
