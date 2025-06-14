using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Category;
using Domain.DTOs.Product;
using Domain.DTOs.Sale;
using Domain.DTOs.StockAdjustment;
using Domain.DTOs.Supplier;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Category, GetCategoryDTO>().ReverseMap();
        CreateMap<Category, CreateCategoryDTO>().ReverseMap();
        CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

        CreateMap<Product, GetProductDTO>().ReverseMap();
        CreateMap<Product, CreateProductDTO>().ReverseMap();
        CreateMap<Product, UpdateProductDTO>().ReverseMap();

        CreateMap<Supplier, GetSupplierDTO>().ReverseMap();
        CreateMap<Supplier, CreateSupplierDTO>().ReverseMap();
        CreateMap<Supplier, UpdateSupplierDTO>().ReverseMap();
        
        CreateMap<Sale, GetSaleDTO>().ReverseMap();
        CreateMap<Sale, CreateSaleDTO>().ReverseMap();
        CreateMap<Sale, UpdateSaleDTO>().ReverseMap();
        
        CreateMap<StockAdjustment, GetStockAdjustmentDTO>().ReverseMap();
        CreateMap<StockAdjustment, CreateStockAdjustmentDTO>().ReverseMap();
        CreateMap<StockAdjustment, UpdateStockAdjustmentDTO>().ReverseMap();
    }
}
