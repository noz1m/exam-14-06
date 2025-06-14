using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.StockAdjustment;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockAssignmentController(IStockAdjustmentService stockAdjustmentService)
{
    [HttpGet]
    public async Task<Response<List<GetStockAdjustmentDTO>>> GetAllAsync()
    {
        return await stockAdjustmentService.GetAllAsync();
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetStockAdjustmentDTO>> GetAsync(int id)
    {
        return await stockAdjustmentService.GetByIdAsync(id);
    }
    [HttpGet("get-stock-adjustment-history")]
    public async Task<Response<List<GetStockAdjustmentDTO>>> GetStockAdjustmentHistoryAsync(int productId)
    {
        return await stockAdjustmentService.GetStockAdjustmentHistoryAsync(productId);
    }
    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateStockAdjustmentDTO stockAdjustment)
    {
        return await stockAdjustmentService.CreateAsync(stockAdjustment);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<string>> UpdateAsync(int id, UpdateStockAdjustmentDTO stockAdjustment)
    {
        return await stockAdjustmentService.UpdateAsync(id, stockAdjustment);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await stockAdjustmentService.DeleteAsync(id);
    }
}
