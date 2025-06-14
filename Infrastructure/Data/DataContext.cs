using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<StockAdjustment> StockAdjustments { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.Id);

        modelBuilder.Entity<Supplier>()
            .HasMany(s => s.Products)
            .WithOne(p => p.Supplier)
            .HasForeignKey(p => p.Id);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Sales)
            .WithOne(s => s.Product)
            .HasForeignKey(s => s.Id);

        modelBuilder.Entity<Product>()
            .HasMany(s => s.StockAdjustments)
            .WithOne(p => p.Product)
            .HasForeignKey(p => p.Id);
    }
}
