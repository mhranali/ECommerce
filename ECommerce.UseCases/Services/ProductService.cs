using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using ECommerce.UseCases.Common;
using ECommerce.UseCases.Contracts;
using ECommerce.UseCases.DTOs.Products;

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

    public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductAsync(CancellationToken ct = default)
    {
        var products = mapper.Map<IReadOnlyList<ProductDto>>(await unitOfWork.GetGenericRepository<Product, int>().GetAllAsync(ct));

        return Result<IReadOnlyList<ProductDto>>.Ok(products);
    }

    public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypeAsync(CancellationToken ct = default)
    {
        var types = mapper.Map<IReadOnlyList<TypeDto>>(await unitOfWork.GetGenericRepository<ProductType, int>().GetAllAsync(ct));

        return Result<IReadOnlyList<TypeDto>>.Ok(types);
    }

    public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
    {
        var product = await unitOfWork.GetGenericRepository<Product, int>().GetByIdAsync(id, ct);

        if (product is null)
            return Error.NotFound("Product.NotFound", $"Product with Id {id} is not found");

        return mapper.Map<ProductDto>(product);
    }
}
