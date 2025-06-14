using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    // navigation properties
    public List<Product> Products { get; set; }
}
