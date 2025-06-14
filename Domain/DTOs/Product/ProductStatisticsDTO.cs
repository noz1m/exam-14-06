namespace Domain.DTOs.Product;

public class ProductStatisticsDTO : GetProductDTO
{
    public int TotalProducts { get; set; }
    public decimal AveragePrice { get; set; }
    public int TotalSold { get; set; }
}
