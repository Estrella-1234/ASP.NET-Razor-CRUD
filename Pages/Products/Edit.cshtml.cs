using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TugasAsinkron1.Data;
using TugasAsinkron1.Models;

namespace TugasAsinkron1.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly TugasAsinkron1.Data.TugasAsinkron1Context _context;

        public EditModel(TugasAsinkron1.Data.TugasAsinkron1Context context)
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

            var product =  await _context.Products.FirstOrDefaultAsync(m => m.ProdukId == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
           ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "NamaSupplier");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
/*            if (!ModelState.IsValid)
            {
                return Page();
            }
*/
            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProdukId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["ExportMessage"] = "Product data has been Edited successfully.";

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProdukId == id)).GetValueOrDefault();
        }
    }
}
