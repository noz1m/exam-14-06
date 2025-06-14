using Domain.ApiResponse;
using Domain.DTOs;
using Domain.DTOs.Category;
using Domain.Filter;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService categoryService)
{
    [HttpGet]
    public async Task<Response<List<GetCategoryDTO>>> GetAllAsync([FromQuery] CategoryFilter filter)
    {
        return await categoryService.GetAllAsync(filter);
    }
    [HttpGet("withProducts")
    ]
    public async Task<Response<List<CategoryWithProductDTO>>> CategoryWithProductsAsync()
    {
        return await categoryService.CategoryWithProductsAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<Response<GetCategoryDTO>> GetByIdAsync(int id)
    {
        return await categoryService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(CreateCategoryDTO category)
    {
        return await categoryService.CreateAsync(category);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<string>> UpdateAsync(int id, UpdateCategoryDTO category)
    {
        return await categoryService.UpdateAsync(id, category);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAsync(int id)
    {
        return await categoryService.DeleteAsync(id);
    }
}
