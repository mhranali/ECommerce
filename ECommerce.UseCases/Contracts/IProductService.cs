using ECommerce.UseCases.Common;
using ECommerce.UseCases.DTOs.Products;
using ECommerce.UseCases.Params;

namespace ECommerce.UseCases.Contracts;

public interface IProductService
{
    Task<Result<PaginatedResult<ProductDto>>> GetAllProductAsync(ProductQueryParams queryParams, CancellationToken ct = default!); 
    Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandAsync(CancellationToken ct = default!); 
    Task<Result<IReadOnlyList<TypeDto>>> GetAllTypeAsync(CancellationToken ct = default!);

    Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default!);
}
