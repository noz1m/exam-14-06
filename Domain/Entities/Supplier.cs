using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Supplier
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
    [MinLength(2, ErrorMessage = "Name cannot be less than 2 characters")]
    public string Name { get; set; }
    [Required]
    [MaxLength(20, ErrorMessage = "Phone cannot be longer than 20 characters")]
    [MinLength(2, ErrorMessage = "Phone cannot be less than 2 characters")]
    public string Phone { get; set; }

    // navigation properties
    public List<Product> Products { get; set; }
}
