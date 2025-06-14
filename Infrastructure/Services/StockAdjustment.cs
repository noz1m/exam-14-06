using System.Net;
using System.Runtime.Intrinsics.Arm;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.StockAdjustment;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class StockAdjustment(DataContext context, IMapper mapper) : IStockAdjustmentService
{
    public async Task<Response<List<GetStockAdjustmentDTO>>> GetAllAsync()
    {
        var stockAdjustment = await context.StockAdjustments.ToListAsync();
        var mapped = mapper.Map<List<GetStockAdjustmentDTO>>(stockAdjustment);
        return new Response<List<GetStockAdjustmentDTO>>(mapped);
    }
    public async Task<Response<GetStockAdjustmentDTO>> GetByIdAsync(int id)
    {
        var stockAdjustment = await context.StockAdjustments.FindAsync(id);
        if (stockAdjustment == null)
        {
            return new Response<GetStockAdjustmentDTO>("Stock Adjustment not found", HttpStatusCode.NotFound);
        }

        var mapped = mapper.Map<GetStockAdjustmentDTO>(stockAdjustment);
        return new Response<GetStockAdjustmentDTO>(mapped);
    }
    public async Task<Response<string>> CreateAsync(CreateStockAdjustmentDTO stockAdjustment)
    {
        if (stockAdjustment == null)
        {
            return new Response<string>("StockAdjustment is null", HttpStatusCode.BadRequest);
        }

        var mapped = mapper.Map<Domain.Entities.StockAdjustment>(stockAdjustment);
        await context.StockAdjustments.AddAsync(mapped);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("StockAdjustment not created", HttpStatusCode.BadRequest)
            : new Response<string>("StockAdjustment created successfully");
    }
    public async Task<Response<string>> UpdateAsync(int id, UpdateStockAdjustmentDTO stockAdjustment)
    {
        if (stockAdjustment == null)
        {
            return new Response<string>("StockAdjustment is null", HttpStatusCode.BadRequest);
        }

        var exist = await context.StockAdjustments.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>("StockAdjustment not found", HttpStatusCode.NotFound);
        }
        mapper.Map(stockAdjustment, exist);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>("StockAdjustment not updated", HttpStatusCode.BadRequest)
            : new Response<string>("StockAdjustment updated successfully");
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var stockAdjustment = await context.StockAdjustments.FindAsync(id);
        if (stockAdjustment == null)
        {
            return new Response<string>("StockAdjustment not found", HttpStatusCode.NotFound);
        }

        context.StockAdjustments.Remove(stockAdjustment);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("StockAdjustment not found", HttpStatusCode.NotFound)
            : new Response<string>("StockAdjustment deleted successfully");
    }

    public async Task<Response<List<GetStockAdjustmentDTO>>> GetStockAdjustmentHistoryAsync(int productId)
    {
        var history = await context.StockAdjustments
       .Where(sa => sa.ProductId == productId)
       .OrderByDescending(sa => sa.AdjustmentDate)
       .Select(sa => new GetStockAdjustmentDTO
       {
           AdjustmentDate = sa.AdjustmentDate,
           Amount = sa.Amount,
           Reason = sa.Reason
       })
       .ToListAsync();

        return new Response<List<GetStockAdjustmentDTO>>(history);
    }
}
