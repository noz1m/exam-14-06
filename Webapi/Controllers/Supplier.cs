using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.Supplier;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Supplier(ISupplierService supplierService)
{
    [HttpGet]
    public async Task<Response<List<GetSupplierDTO>>> GetAllAsync([FromQuery] SupplierFilter filter)
    {
        return await supplierService.GetAllAsync(filter);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetSupplierDTO>> GetByIdAsync(int id)
    {
        return await supplierService.GetByIdAsync(id);
    }
    [HttpGet("supplier-with-products")]
    public async Task<Response<List<GetSupplierDTO>>> GetSuppliersWithProductsAsync()
    {
        return await supplierService.GetSuppliersWithProductsAsync();
    }
    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateSupplierDTO supplier)
    {
        return await supplierService.CreateAsync(supplier);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<string>> UpdateAsync(int id, UpdateSupplierDTO supplier)
    {
        return await supplierService.UpdateAsync(id, supplier);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await supplierService.DeleteAsync(id);
    }
}
