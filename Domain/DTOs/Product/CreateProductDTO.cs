namespace Domain.DTOs.Product;

public class CreateProductDTO
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public int QuantitySold { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
}
