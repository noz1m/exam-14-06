using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class StockAdjustment
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AdjustmentAmount { get; set; }
    public string Reason { get; set; }
    public int Amount { get; set; }
    public DateTime AdjustmentDate { get; set; }

    // navigation properties
    public Product Product { get; set; }
    public List<Sale> Sales { get; set; }
}
