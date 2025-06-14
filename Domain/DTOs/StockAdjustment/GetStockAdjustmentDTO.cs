namespace Domain.DTOs.StockAdjustment;

public class GetStockAdjustmentDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AdjustmentAmount { get; set; }
    public string Reason { get; set; }
    public int Amount { get; set; }
    public DateTime AdjustmentDate { get; set; }
}
