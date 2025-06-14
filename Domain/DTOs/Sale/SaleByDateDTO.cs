namespace Domain.DTOs.Sale;

public class SaleByDateDTO : GetSaleDTO
{
    public string ProductName { get; set; }
    public int TotalSold { get; set; }
    public DateTime Date { get; set; }
    public decimal Revenue { get; set; }
}
