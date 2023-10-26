using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TugasAsinkron1.Data;
using TugasAsinkron1.Models;

namespace TugasAsinkron1.Pages.Suppliers
{
    public class IndexModel : PageModel
    {
        private readonly TugasAsinkron1.Data.TugasAsinkron1Context _context;

        public IndexModel(TugasAsinkron1.Data.TugasAsinkron1Context context)
        {
            _context = context;
        }

        public IList<Supplier> Supplier { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Suppliers != null)
            {
                Supplier = await _context.Suppliers.ToListAsync();
            }
        }

        public async Task<IActionResult> OnGetExportToJson()
        {
            var suppliers = await _context.Suppliers
                .Include(s => s.Products)
                .ToListAsync();

            var supplierList = suppliers.Select(supplier => new
            {
                SupplierId = supplier.SupplierId,
                NamaSupplier = supplier.NamaSupplier,
                Alamat = supplier.Alamat,
                Tlp = supplier.Tlp,
                Products = supplier.Products.Select(product => new
                {
                    ProductId = product.ProdukId,
                    NamaProduk = product.NamaProduk,
                    Deskripsi = product.Deskripsi,
                    Qty = product.Qty,
                    Satuan = product.Satuan,
                    Harga = product.Harga
                }).ToList()
            }).ToList();

  

            var suppliersJson = JsonConvert.SerializeObject(supplierList, Formatting.Indented);
            var byteArray = Encoding.UTF8.GetBytes(suppliersJson);

            var filePath = Path.Combine(Environment.CurrentDirectory, "Stores/Suppliers/suppliers.json");

            // Membuat direktori jika tidak ada
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Menyimpan JSON ke file
            System.IO.File.WriteAllText(filePath, suppliersJson);

            TempData["ExportMessage"] = "Supplier data has been exported successfully.";
			// Tambahkan kode JavaScript untuk memicu toast
			TempData["ShowToast"] = "true";

			return new FileContentResult(byteArray, "application/json")
			{
				FileDownloadName = "suppliers.json"
			};
		}
    }
}
