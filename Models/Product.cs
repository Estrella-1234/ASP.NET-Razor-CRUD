using System;
using System.Collections.Generic;

namespace TugasAsinkron1.Models;

public partial class Product
{
    public int ProdukId { get; set; }

    public string NamaProduk { get; set; } = null!;

    public int SupplierId { get; set; }

    public string Deskripsi { get; set; } = null!;

    public int Qty { get; set; }

    public string Satuan { get; set; } = null!;

    public string Harga { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
