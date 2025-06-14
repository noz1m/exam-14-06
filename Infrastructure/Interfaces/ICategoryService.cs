using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.Category;
using Domain.Filter;

namespace Infrastructure.Interfaces;

public interface ICategoryService
{
    Task<PagedResponse<List<GetCategoryDTO>>> GetAllAsync(CategoryFilter filter);
    Task<Response<List<CategoryWithProductDTO>>> CategoryWithProductsAsync();
    Task<Response<GetCategoryDTO>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(CreateCategoryDTO category);
    Task<Response<string>> UpdateAsync(int id,UpdateCategoryDTO category);
    Task<Response<string>> DeleteAsync(int id);
}
