using System;
using System.Collections.Generic;

namespace TugasAsinkron1.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string NamaSupplier { get; set; } = null!;

    public string Alamat { get; set; } = null!;

    public string Tlp { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
