using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TugasAsinkron1.Models;

namespace TugasAsinkron1.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly TugasAsinkron1.Data.TugasAsinkron1Context _context;


		public IndexModel(TugasAsinkron1.Data.TugasAsinkron1Context context)
        {
            _context = context;

		}

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Product = await _context.Products
                .Include(p => p.Supplier).ToListAsync();
            }
        }

		public async Task<IActionResult> OnGetExportToJson()
		{
			Console.WriteLine("Exporting to JSON");
			var products = await _context.Products.Include(p => p.Supplier).ToListAsync();
			Console.WriteLine(products);


			var jsonArray = new List<object>();

			foreach (var product in products)
			{
				var productInfo = new
				{
					NamaProduk = product.NamaProduk,
					Deskripsi = product.Deskripsi,
					Qty = product.Qty,
					Satuan = product.Satuan,
					Harga = product.Harga,
					NamaSupplier = product.Supplier.NamaSupplier,
					Alamat = product.Supplier.Alamat,
					Tlp = product.Supplier.Tlp
				};

				jsonArray.Add(productInfo);
			}

			var jsonString = JsonConvert.SerializeObject(jsonArray, Formatting.Indented);

			var filePath = Path.Combine(Environment.CurrentDirectory, "Stores/Products/products.json");

			// Membuat direktori jika tidak ada
			var directoryPath = Path.GetDirectoryName(filePath);
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			// Menyimpan JSON ke file
			System.IO.File.WriteAllText(filePath, jsonString);

			// Menambahkan pesan flash
			TempData["ExportMessage"] = "Data has been exported successfully.";

			return RedirectToAction("Index");

		}




		public IActionResult OnPostExportToJson()
		{
			Console.WriteLine("Exporting to JSON2");
			// Serialize the data to JSON
			var json = JsonConvert.SerializeObject(Product);

			// Return a ContentResult with the JSON data
			return Content(json, "application/json");
		}

	}
}
