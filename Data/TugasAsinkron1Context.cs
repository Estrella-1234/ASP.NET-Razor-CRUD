using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TugasAsinkron1.Models;

namespace TugasAsinkron1.Data;

public partial class TugasAsinkron1Context : DbContext
{
    public TugasAsinkron1Context()
    {
    }

    public TugasAsinkron1Context(DbContextOptions<TugasAsinkron1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProdukId);

            entity.HasIndex(e => e.SupplierId, "IX_Products_SupplierId");

            entity.Property(e => e.Deskripsi).HasColumnName("deskripsi");
            entity.Property(e => e.Harga)
                .HasMaxLength(255)
                .HasColumnName("harga");
            entity.Property(e => e.NamaProduk)
                .HasMaxLength(100)
                .HasColumnName("namaProduk");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Satuan)
                .HasMaxLength(10)
                .HasColumnName("satuan");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products).HasForeignKey(d => d.SupplierId);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.Alamat)
                .HasMaxLength(100)
                .HasColumnName("alamat");
            entity.Property(e => e.NamaSupplier)
                .HasMaxLength(50)
                .HasColumnName("namaSupplier");
            entity.Property(e => e.Tlp)
                .HasMaxLength(20)
                .HasColumnName("tlp");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
