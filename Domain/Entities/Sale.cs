using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Sale
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int QuantitySolid { get; set; }
    public DateTime SaleDate { get; set; }

    // navigation properties
    public Product Product { get; set; }
    public List<StockAdjustment> StockAdjustments { get; set; }
}
