namespace Domain.DTOs.StockAdjustment;

public class CreateStockAdjustmentDTO
{
    public int ProductId { get; set; }
    public int AdjustmentAmount { get; set; }
    public string Reason { get; set; }
    public DateTime AdjustmentDate { get; set; }
}
