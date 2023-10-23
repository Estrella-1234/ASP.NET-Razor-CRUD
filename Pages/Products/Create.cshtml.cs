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
    public class CreateModel : PageModel
    {
        private readonly TugasAsinkron1.Data.TugasAsinkron1Context _context;

        public CreateModel(TugasAsinkron1.Data.TugasAsinkron1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "NamaSupplier");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Cari objek Supplier berdasarkan SupplierId yang dipilih dalam form
            Product.Supplier = await _context.Suppliers.FindAsync(Product.SupplierId);


/*            Console.WriteLine(Product.Supplier.SupplierId);
            Console.WriteLine(Product.Supplier.NamaSupplier);
            Console.WriteLine(Product.Supplier.NamaSupplier);*/

            if (Product.Supplier == null)
            {
                // Handle kasus ketika Supplier dengan ID yang dipilih tidak ditemukan
                ModelState.AddModelError("Product.SupplierId", "Supplier not found");
                ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId");
                return Page();
            }

/*            if (!ModelState.IsValid)
            {
                return Page();
            }*/

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            TempData["ExportMessage"] = "Product data has been Created successfully.";

            return RedirectToPage("./Index");
        }


    }
}
