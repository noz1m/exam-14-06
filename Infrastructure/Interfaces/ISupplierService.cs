using Domain.ApiResponse;
using Domain.DTOs.Supplier;
using Domain.Filter;

namespace Infrastructure.Interfaces;

public interface ISupplierService
{
    Task<PagedResponse<List<GetSupplierDTO>>> GetAllAsync(SupplierFilter filter);
    Task<Response<List<GetSupplierDTO>>> GetSuppliersWithProductsAsync();
    Task<Response<GetSupplierDTO>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(CreateSupplierDTO supplier);
    Task<Response<string>> UpdateAsync(int id,UpdateSupplierDTO supplier);
    Task<Response<string>> DeleteAsync(int id);
}
