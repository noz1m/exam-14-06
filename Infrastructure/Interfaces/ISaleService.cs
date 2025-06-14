using Domain.ApiResponse;
using Domain.DTOs.Sale;
using Domain.Filter;

namespace Infrastructure.Interfaces;

public interface ISaleService
{
    Task<PagedResponse<List<GetSaleDTO>>> GetAllAsync(SaleFilter filter);
    Task<Response<GetSaleDTO>> GetAsync(int id);
    Task<Response<List<SaleByDateDTO>>> GetSalesByDateAsync(DateTime fromDate, DateTime toDate);
    Task<Response<List<SaleByDateDTO>>> GetTopSoldProductsAsync();
    Task<Response<List<SaleByDateDTO>>> GetDailyRevenueAsync();
    Task<Response<string>> CreateAsync(CreateSaleDTO sale);
    Task<Response<string>> UpdateAsync(int id,UpdateSaleDTO sale);
    Task<Response<string>> DeleteAsync(int id);
}
