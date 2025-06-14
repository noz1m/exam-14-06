using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.Sale;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleController(ISaleService saleService)
{
    [HttpGet]
    public async Task<Response<List<GetSaleDTO>>> GetAllAsync([FromQuery] SaleFilter filter)
    {
        return await saleService.GetAllAsync(filter);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetSaleDTO>> GetAsync(int id)
    {
        return await saleService.GetAsync(id);
    }
    [HttpGet("get-sales-with-date")]
    public async Task<Response<List<SaleByDateDTO>>> GetSalesByDateAsync(DateTime fromDate, DateTime toDate)
    {
        return await saleService.GetSalesByDateAsync(fromDate, toDate);
    }
    [HttpGet("get-top-sold-products")]
    public async Task<Response<List<SaleByDateDTO>>> GetTopSoldProductsAsync()
    {
        return await saleService.GetTopSoldProductsAsync();
    }
    [HttpGet("get-daily-revenue")]
    public async Task<Response<List<SaleByDateDTO>>> GetDailyRevenueAsync()
    {
        return await saleService.GetDailyRevenueAsync();
    }
    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateSaleDTO sale)
    {
        return await saleService.CreateAsync(sale);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<string>> UpdateAsync(int id, UpdateSaleDTO sale)
    {
        return await saleService.UpdateAsync(id, sale);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await saleService.DeleteAsync(id);
    }
}   
