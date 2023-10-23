using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TugasAsinkron1.Data;
using TugasAsinkron1.Models;

namespace TugasAsinkron1.Pages.Suppliers
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
            return Page();
        }

        [BindProperty]
        public Supplier Supplier { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Suppliers == null || Supplier == null)
            {
                return Page();
            }

            _context.Suppliers.Add(Supplier);
            await _context.SaveChangesAsync();

			TempData["ExportMessage"] = "Supplier data has been Created successfully.";

			return RedirectToPage("./Index");
        }
    }
}
