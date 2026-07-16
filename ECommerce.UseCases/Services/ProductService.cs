using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using ECommerce.UseCases.Common;
using ECommerce.UseCases.Contracts;
using ECommerce.UseCases.DTOs.Products;
using ECommerce.UseCases.Params;
using ECommerce.UseCases.Specifications;

namespace ECommerce.UseCases.Services;

internal class ProductService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IProductService
{
    public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandAsync(CancellationToken ct = default)
    {
        var brands = await unitOfWork.GetGenericRepository<ProductBrand, int>().GetAllAsync(ct);
        //var data = mapper.Map<Result<IReadOnlyList<BrandDto>>>(brands);
        var data = mapper.Map<IReadOnlyList<BrandDto>>(brands);

        return Result<IReadOnlyList<BrandDto>>.Ok(data);
    }
    public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypeAsync(CancellationToken ct = default)
    {
        var types = mapper.Map<IReadOnlyList<TypeDto>>(await unitOfWork.GetGenericRepository<ProductType, int>().GetAllAsync(ct));

        return Result<IReadOnlyList<TypeDto>>.Ok(types);
    }

    public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductAsync(ProductQueryParams queryParams, CancellationToken ct = default)
    {
        var specifications = new ProductSpecifications(queryParams);

        var products = await unitOfWork.GetGenericRepository<Product, int>().GetAllWithSpecificationsAsync(specifications, ct);

        var data = mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDto>>(products);

        var countSpecifications = new ProductCountSpecification(queryParams);

        var totalCount = await unitOfWork.GetGenericRepository<Product, int>().GetCountWithSpecificationsAsync(countSpecifications, ct);

        if (totalCount <= 0)
            return Error.NotFound("Products.NotFound");

        var result = new PaginatedResult<ProductDto>(data, queryParams.pageIndex, data.Count, totalCount);

        return Result<PaginatedResult<ProductDto>>.Ok(result);
    }


    public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
    {
        var specifications = new ProductSpecifications(id);

        var product = await unitOfWork.GetGenericRepository<Product, int>().GetByIdWithSpecificationAsync(specifications, ct);

        if (product is null)
            return Error.NotFound("Product.NotFound", $"Product with Id {id} is not found");

        return mapper.Map<ProductDto>(product);
        
    }
}
