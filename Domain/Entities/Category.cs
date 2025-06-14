using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    [MinLength(3, ErrorMessage = "Name should have at least 3 characters")]
    public string Name { get; set; }

    // navigation properties
    public List<Product> Products { get; set; }
}
