using System.Net;
using System.Runtime.Intrinsics.Arm;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.Category;
using Domain.DTOs.Product;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CategoryService(DataContext context, IMapper mapper) : ICategoryService
{
    public async Task<PagedResponse<List<GetCategoryDTO>>> GetAllAsync(CategoryFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);
        var categories = context.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
        {
            categories = categories.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }

        var totalRecords = await categories.CountAsync();

        var paged = await categories
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

        var mapped = mapper.Map<List<GetCategoryDTO>>(paged);

        return new PagedResponse<List<GetCategoryDTO>>(mapped, validFilter.PageNumber, validFilter.PageSize, totalRecords);
    }
    public async Task<Response<GetCategoryDTO>> GetByIdAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null)
        {
            return new Response<GetCategoryDTO>("Category not found", HttpStatusCode.NotFound);
        }

        var mapped = mapper.Map<GetCategoryDTO>(category);
        return new Response<GetCategoryDTO>(mapped);
    }
    public async Task<Response<string>> CreateAsync(CreateCategoryDTO category)
    {
        if (category == null)
        {
            return new Response<string>("Category is null", HttpStatusCode.BadRequest);
        }

        var mapped = mapper.Map<CreateCategoryDTO, Category>(category);
        await context.Categories.AddAsync(mapped);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Category not created", HttpStatusCode.BadRequest)
            : new Response<string>("Category created successfully");
    }
    public async Task<Response<string>> UpdateAsync(int id, UpdateCategoryDTO category)
    {
        if (category == null)
        {
            return new Response<string>("Category is null", HttpStatusCode.BadRequest);
        }

        var exist = await context.Categories.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>("Category not found", HttpStatusCode.NotFound);
        }
        mapper.Map(category, exist);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>("Category not updated", HttpStatusCode.BadRequest)
            : new Response<string>("Category updated successfully");
    }
    public async Task<Response<string>> DeleteAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null)
        {
            return new Response<string>("Category not found", HttpStatusCode.NotFound);
        }

        context.Categories.Remove(category);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Category not found", HttpStatusCode.NotFound)
            : new Response<string>("Category deleted successfully");
    }

    public async Task<Response<List<CategoryWithProductDTO>>> CategoryWithProductsAsync()
    {
        var categories = await context.Categories
            .Include(c => c.Products)
            .Select(c => new CategoryWithProductDTO
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => new GetProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList()
            })
            .ToListAsync();

        return new Response<List<CategoryWithProductDTO>>(categories);
    }
}
