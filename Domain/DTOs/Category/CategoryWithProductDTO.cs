using Domain.DTOs.Product;

namespace Domain.DTOs.Category;

public class CategoryWithProductDTO : GetCategoryDTO
{
    public List<GetProductDTO> Products  { get; set; }
}
