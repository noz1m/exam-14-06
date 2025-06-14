using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.Product;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService)
{
    [HttpGet]
    public async Task<Response<List<GetProductDTO>>> GetAllAsync([FromQuery] ProductFilter filter)
    {
        return await productService.GetAllAsync(filter);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetProductDTO>> GetAsync(int id)
    {
        return await productService.GetAsync(id);
    }
    [HttpGet("low-stock")]
    public async Task<Response<List<GetProductDTO>>> GetLowStockProductsAsync()
    {
        return await productService.GetLowStockProductsAsync();
    }
    [HttpGet("statistics")]
    public async Task<Response<DashboardStatisticsDTO>> GetStatisticsAsync()
    {
        return await productService.GetStatisticsAsync();
    }
    [HttpGet("product-statistics")]
    public async Task<Response<ProductStatisticsDTO>> GetProductStatisticsAsync()
    {
        return await productService.GetProductStatisticsAsync();
    }
    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateProductDTO product)
    {
        return await productService.CreateAsync(product);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<string>> UpdateAsync(int id, UpdateProductDTO product)
    {
        return await productService.UpdateAsync(id, product);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await productService.DeleteAsync(id);
    }
}
