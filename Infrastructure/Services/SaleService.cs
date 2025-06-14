using System.Net;
using System.Runtime.Intrinsics.Arm;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.Sale;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SaleService(DataContext context, IMapper mapper) : ISaleService
{
    public async Task<PagedResponse<List<GetSaleDTO>>> GetAllAsync(SaleFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);
        var sales = context.Sales.AsQueryable();

        if (filter.MinDate != null)
        {
            sales = sales.Where(x => x.SaleDate >= filter.MinDate);
        }

        if (filter.MaxDate != null)
        {
            sales = sales.Where(x => x.SaleDate <= filter.MaxDate);
        }

        if (filter.ProductId != null && filter.ProductId > 0)
        {
            sales = sales.Where(x => x.ProductId == filter.ProductId);
        }

        var totalRecords = await sales.CountAsync();

        var paged = await sales
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

        var mapped = mapper.Map<List<GetSaleDTO>>(paged);

        return new PagedResponse<List<GetSaleDTO>>(mapped, validFilter.PageNumber, validFilter.PageSize, totalRecords);
    }
    public async Task<Response<GetSaleDTO>> GetAsync(int id)
    {
        var sale = await context.Sales.FindAsync(id);
        if (sale == null)
        {
            return new Response<GetSaleDTO>("Sale not found", HttpStatusCode.NotFound);
        }

        var mapped = mapper.Map<GetSaleDTO>(sale);
        return new Response<GetSaleDTO>(mapped);
    }
    public async Task<Response<string>> CreateAsync(CreateSaleDTO sale)
    {
        if (!await context.Products.AnyAsync(p => p.Id == sale.ProductId))
        {
            return new Response<string>("Product not found", HttpStatusCode.BadRequest);
        }

        if (sale == null)
        {
            return new Response<string>("Sale is null", HttpStatusCode.BadRequest);
        }

        var mapped = mapper.Map<CreateSaleDTO, Sale>(sale);
        await context.Sales.AddAsync(mapped);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Sale not created", HttpStatusCode.BadRequest)
            : new Response<string>("Sale created successfully");
    }
    public async Task<Response<string>> UpdateAsync(int id, UpdateSaleDTO sale)
    {
        if (sale == null)
        {
            return new Response<string>("Sale is null", HttpStatusCode.BadRequest);
        }

        var exist = await context.Sales.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>("Sale not found", HttpStatusCode.NotFound);
        }
        mapper.Map(sale, exist);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>("Sale not updated", HttpStatusCode.BadRequest)
            : new Response<string>("Sale updated successfully");
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var sale = await context.Sales.FindAsync(id);
        if (sale == null)
        {
            return new Response<string>("Sale not found", HttpStatusCode.NotFound);
        }

        context.Sales.Remove(sale);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Sale not found", HttpStatusCode.NotFound)
            : new Response<string>("Sale deleted successfully");
    }

    public async Task<Response<List<SaleByDateDTO>>> GetSalesByDateAsync(DateTime fromDate, DateTime toDate)
    {
        var sales = await context.Sales
                .Include(s => s.Product)
                .Where(s => s.SaleDate >= fromDate && s.SaleDate <= toDate)
                .Select(s => new SaleByDateDTO
                {
                    Id = s.Id,
                    ProductName = s.Product.Name,
                    QuantitySolid = s.QuantitySolid,
                    SaleDate = s.SaleDate
                })
                .ToListAsync();

        return new Response<List<SaleByDateDTO>>(sales);
    }

    public async Task<Response<List<SaleByDateDTO>>> GetTopSoldProductsAsync()
    {
        var topProducts = await context.Sales
                .GroupBy(s => s.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSold = g.Sum(s => s.QuantitySolid)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .Join(context.Products,
                    sale => sale.ProductId,
                    product => product.Id,
                    (sale, product) => new SaleByDateDTO
                    {
                        ProductName = product.Name,
                        TotalSold = sale.TotalSold
                    })
                .ToListAsync();

        return new Response<List<SaleByDateDTO>>(topProducts);
    }

    public async Task<Response<List<SaleByDateDTO>>> GetDailyRevenueAsync()
    {
        var fromDate = DateTime.Today.AddDays(-6);

        var revenueByDay = await context.Sales
            .Where(s => s.SaleDate.Date >= fromDate)
            .Include(s => s.Product)
            .GroupBy(s => s.SaleDate.Date)
            .Select(g => new SaleByDateDTO
            {
                Date = g.Key,
                Revenue = g.Sum(s => s.QuantitySolid * s.Product.Price)
            })
            .OrderBy(r => r.Date)
            .ToListAsync();

        return new Response<List<SaleByDateDTO>>(revenueByDay);
    }
}
