namespace Domain.DTOs.Sale;

public class GetSaleDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int QuantitySolid { get; set; }
    public DateTime SaleDate { get; set; }
}
