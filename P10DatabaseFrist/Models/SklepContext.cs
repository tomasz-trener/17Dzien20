using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace P10DatabaseFrist.Models;

public partial class SklepContext : DbContext
{
    public SklepContext()
    {
    }

    public SklepContext(DbContextOptions<SklepContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaterialCategory> MaterialCategories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAdjective> ProductAdjectives { get; set; }

    public virtual DbSet<ProductProductAdjective> ProductProductAdjectives { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=sklep;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.MaterialCategoryId, "IX_Products_MaterialCategoryId");

            entity.Property(e => e.Color).HasMaxLength(50);

            entity.HasOne(d => d.MaterialCategory).WithMany(p => p.Products).HasForeignKey(d => d.MaterialCategoryId);
        });

        modelBuilder.Entity<ProductProductAdjective>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.ProductAdjectiveId });

            entity.ToTable("Product_ProductAdjectives");

            entity.HasIndex(e => e.ProductAdjectiveId, "IX_Product_ProductAdjectives_ProductAdjectiveId");

            entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.ProductAdjective).WithMany(p => p.ProductProductAdjectives).HasForeignKey(d => d.ProductAdjectiveId);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductProductAdjectives).HasForeignKey(d => d.ProductId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
