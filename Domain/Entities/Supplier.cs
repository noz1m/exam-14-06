using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Supplier
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }

    // navigation properties
    public List<Product> Products { get; set; }
}
