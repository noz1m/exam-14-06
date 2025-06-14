using System.Net;
using System.Runtime.Intrinsics.Arm;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.Supplier;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SupplierService(DataContext context, IMapper mapper) : ISupplierService
{
    public async Task<PagedResponse<List<GetSupplierDTO>>> GetAllAsync(SupplierFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);
        var suppliers = context.Suppliers.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
        {
            suppliers = suppliers.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }

        if (filter.Phone == null)
        {
            suppliers = suppliers.Where(x => x.Phone == filter.Phone);
        }

        var totalRecords = await suppliers.CountAsync();

        var paged = await suppliers
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

        var mapped = mapper.Map<List<GetSupplierDTO>>(paged);

        return new PagedResponse<List<GetSupplierDTO>>(mapped, validFilter.PageNumber, validFilter.PageSize, totalRecords);
    }
    public async Task<Response<GetSupplierDTO>> GetByIdAsync(int id)
    {
        var supplier = await context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return new Response<GetSupplierDTO>("Supplier not found", HttpStatusCode.NotFound);
        }

        var mapped = mapper.Map<GetSupplierDTO>(supplier);
        return new Response<GetSupplierDTO>(mapped);
    }
    public async Task<Response<string>> CreateAsync(CreateSupplierDTO supplier)
    {
        if (supplier == null)
        {
            return new Response<string>("Suppliers is null", HttpStatusCode.BadRequest);
        }

        var mapped = mapper.Map<CreateSupplierDTO, Supplier>(supplier);
        await context.Suppliers.AddAsync(mapped);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Suppliers not created", HttpStatusCode.BadRequest)
            : new Response<string>("Suppliers created successfully");
    }
    public async Task<Response<string>> UpdateAsync(int id, UpdateSupplierDTO supplier)
    {
        if (supplier == null)
        {
            return new Response<string>("Suppliers is null", HttpStatusCode.BadRequest);
        }

        var exist = await context.Suppliers.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>("Suppliers not found", HttpStatusCode.NotFound);
        }
        mapper.Map(supplier, exist);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>("Suppliers not updated", HttpStatusCode.BadRequest)
            : new Response<string>("Suppliers updated successfully");
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var supplier = await context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return new Response<string>("Suppliers not found", HttpStatusCode.NotFound);
        }

        context.Suppliers.Remove(supplier);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Suppliers not found", HttpStatusCode.NotFound)
            : new Response<string>("Suppliers deleted successfully");
    }

    public async Task<Response<List<GetSupplierDTO>>> GetSuppliersWithProductsAsync()
    {
        var suppliers = await context.Suppliers
        .Include(s => s.Products)
        .Select(s => new GetSupplierDTO
        {
            Id = s.Id,
            Name = s.Name,
            Products = s.Products.Select(p => p.Name).ToList()
        })
        .ToListAsync();

    return new Response<List<GetSupplierDTO>>(suppliers);
    }
}
