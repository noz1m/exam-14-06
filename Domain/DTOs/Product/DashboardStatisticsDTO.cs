namespace Domain.DTOs.Product;

public class DashboardStatisticsDTO : ProductStatisticsDTO
{
    public decimal TotalRevenue { get; set; }
    public int TotalSales { get; set; }
}
