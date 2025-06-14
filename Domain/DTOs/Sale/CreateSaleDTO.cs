namespace Domain.DTOs.Sale;

public class CreateSaleDTO
{
    public int ProductId { get; set; }
    public int QuantitySolid { get; set; }
    public DateTime SaleDate { get; set; }
}
