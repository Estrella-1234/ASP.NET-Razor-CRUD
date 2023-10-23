using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TugasAsinkron1.Data;
using TugasAsinkron1.Models;

namespace TugasAsinkron1.Pages.Suppliers
{
    public class DetailsModel : PageModel
    {
        private readonly TugasAsinkron1.Data.TugasAsinkron1Context _context;

        public DetailsModel(TugasAsinkron1.Data.TugasAsinkron1Context context)
        {
            _context = context;
        }

      public Supplier Supplier { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FirstOrDefaultAsync(m => m.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }
            else 
            {
                Supplier = supplier;
            }
            return Page();
        }
    }
}
