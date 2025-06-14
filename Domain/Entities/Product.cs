using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public int QuantitySold { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }

    // navigation properties
    public Category Category { get; set; }
    public Supplier Supplier { get; set; }
    public List<StockAdjustment> StockAdjustments { get; set; }
    public List<Sale> Sales { get; set; }
}
