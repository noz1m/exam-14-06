using Domain.ApiResponse;
using Domain.DTOs.StockAdjustment;

namespace Infrastructure.Interfaces;

public interface IStockAdjustmentService
{
    Task<Response<List<GetStockAdjustmentDTO>>> GetAllAsync();
    Task<Response<List<GetStockAdjustmentDTO>>> GetStockAdjustmentHistoryAsync(int productId);
    Task<Response<GetStockAdjustmentDTO>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(CreateStockAdjustmentDTO stockAdjustment);
    Task<Response<string>> UpdateAsync(int id, UpdateStockAdjustmentDTO stockAdjustment);
    Task<Response<string>> DeleteAsync(int id);
}
