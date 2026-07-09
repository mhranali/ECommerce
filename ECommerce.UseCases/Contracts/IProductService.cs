using ECommerce.UseCases.Common;
using ECommerce.UseCases.DTOs.Products;

namespace ECommerce.UseCases.Contracts;

public interface IProductService
{
    Task<Result<IReadOnlyList<ProductDto>>> GetAllProductAsync(CancellationToken ct = default!); 
    Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandAsync(CancellationToken ct = default!); 
    Task<Result<IReadOnlyList<TypeDto>>> GetAllTypeAsync(CancellationToken ct = default!);

    Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default!);
}
