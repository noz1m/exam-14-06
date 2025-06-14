namespace Domain.DTOs.Supplier;

public class GetSupplierDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public List<string> Products { get; set; }
}
