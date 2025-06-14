using System.Net;
using System.Runtime.Intrinsics.Arm;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.Product;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService(DataContext context, IMapper mapper) : IProductService
{
    public async Task<PagedResponse<List<GetProductDTO>>> GetAllAsync(ProductFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);
        var products = context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
        {
            products = products.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }

        if (filter.MinPrice != null)
        {
            products = products.Where(x => x.Price >= filter.MinPrice);
        }

        if (filter.MaxPrice != null)
        {
            products = products.Where(x => x.Price <= filter.MaxPrice);
        }

        if (filter.CategoryId != null && filter.CategoryId > 0)
        {
            products = products.Where(x => x.CategoryId == filter.CategoryId);
        }

        if (filter.SupplierId != null && filter.SupplierId > 0)
        {
            products = products.Where(x => x.SupplierId == filter.SupplierId);
        }

        var totalRecords = await products.CountAsync();

        var paged = await products
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

        var mapped = mapper.Map<List<GetProductDTO>>(paged);

        return new PagedResponse<List<GetProductDTO>>(mapped, validFilter.PageNumber, validFilter.PageSize, totalRecords);
    }
    public async Task<Response<GetProductDTO>> GetAsync(int id)
    {
        var products = await context.Products.FindAsync(id);
        if (products == null)
        {
            return new Response<GetProductDTO>("Product not found", HttpStatusCode.NotFound);
        }

        var mapped = mapper.Map<GetProductDTO>(products);
        return new Response<GetProductDTO>(mapped);
    }
    public async Task<Response<string>> CreateAsync(CreateProductDTO product)
    {
        if (!await context.Categories.AnyAsync(c => c.Id == product.CategoryId))
        {
            return new Response<string>("Category not found", HttpStatusCode.BadRequest);
        }

        if (!await context.Suppliers.AnyAsync(s => s.Id == product.SupplierId))
        {
            return new Response<string>("Supplier not found", HttpStatusCode.BadRequest);
        }

        if (product == null)
        {
            return new Response<string>("Product is null", HttpStatusCode.BadRequest);
        }

        var mapped = mapper.Map<CreateProductDTO, Product>(product);
        await context.Products.AddAsync(mapped);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Product not created", HttpStatusCode.BadRequest)
            : new Response<string>("Product created successfully");
    }
    public async Task<Response<string>> UpdateAsync(int id, UpdateProductDTO product)
    {
        if (!await context.Categories.AnyAsync(c => c.Id == product.CategoryId))
        {
            return new Response<string>("Category not found", HttpStatusCode.BadRequest);
        }

        if (!await context.Suppliers.AnyAsync(s => s.Id == product.SupplierId))
        {
            return new Response<string>("Supplier not found", HttpStatusCode.BadRequest);
        }

        if (product == null)
        {
            return new Response<string>("Product is null", HttpStatusCode.BadRequest);
        }

        var exist = await context.Products.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>("Product not found", HttpStatusCode.NotFound);
        }
        mapper.Map(product, exist);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>("Product not updated", HttpStatusCode.BadRequest)
            : new Response<string>("Product updated successfully");
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
        {
            return new Response<string>("Category not found", HttpStatusCode.NotFound);
        }

        context.Products.Remove(product);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Category not found", HttpStatusCode.NotFound)
            : new Response<string>("Category deleted successfully");
    }

    public async Task<Response<List<GetProductDTO>>> GetLowStockProductsAsync()
    {
        var products = await context.Products
        .Where(p => p.QuantityInStock < 5)
        .Select(p => new GetProductDTO
        {
            Id = p.Id,
            Name = p.Name,
            QuantityInStock = p.QuantityInStock
        })
        .ToListAsync();

        return new Response<List<GetProductDTO>>(products);
    }

    public async Task<Response<ProductStatisticsDTO>> GetProductStatisticsAsync()
    {
        var totalProducts = await context.Products.CountAsync();
        var averagePrice = await context.Products.AverageAsync(p => p.Price);
        var totalSold = await context.Products.SumAsync(p => p.QuantitySold);

        var stats = new ProductStatisticsDTO
        {
            TotalProducts = totalProducts,
            AveragePrice = averagePrice,
            TotalSold = totalSold
        };

        return new Response<ProductStatisticsDTO>(stats);
    }

    public async Task<Response<DashboardStatisticsDTO>> GetStatisticsAsync()
    {
        var totalProducts = await context.Products.CountAsync();
        var totalSales = await context.Sales.CountAsync();
        var totalRevenue = await context.Sales
            .Include(s => s.Product)
            .SumAsync(s => s.QuantitySolid * s.Product.Price);

        var result = new DashboardStatisticsDTO
        {
            TotalProducts = totalProducts,
            TotalSales = totalSales,
            TotalRevenue = totalRevenue
        };

        return new Response<DashboardStatisticsDTO>(result);
    }
}
