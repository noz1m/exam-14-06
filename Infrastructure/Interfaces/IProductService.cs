using Domain.ApiResponse;
using Domain.DTOs.Product;
using Domain.Filter;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<PagedResponse<List<GetProductDTO>>> GetAllAsync(ProductFilter filter);
    Task<Response<List<GetProductDTO>>> GetLowStockProductsAsync();
    Task<Response<ProductStatisticsDTO>> GetProductStatisticsAsync();
    Task<Response<DashboardStatisticsDTO>> GetStatisticsAsync();
    Task<Response<GetProductDTO>> GetAsync(int id);
    Task<Response<string>> CreateAsync(CreateProductDTO product);
    Task<Response<string>> UpdateAsync(int id,UpdateProductDTO product);
    Task<Response<string>> DeleteAsync(int id);
}
