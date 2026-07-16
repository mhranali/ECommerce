using ECommerce.Domain.Entities.Products;
using ECommerce.UseCases.Params;

namespace ECommerce.UseCases.Specifications;

public class ProductCountSpecification : BaseSpecifications<Product, int>
{
    public ProductCountSpecification(ProductQueryParams queryParams)
        : base(p =>
        (!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId) &&
        (!queryParams.typeId.HasValue || p.TypeId == queryParams.typeId) &&
        (string.IsNullOrEmpty(queryParams.searchValue) || p.Name.ToLower().Contains(queryParams.searchValue.ToLower())))
    {

    }


}
