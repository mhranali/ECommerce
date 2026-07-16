using ECommerce.Domain.Entities.Products;
using ECommerce.UseCases.Params;

namespace ECommerce.UseCases.Specifications;

public class ProductSpecifications : BaseSpecifications<Product, int>
{
    public ProductSpecifications(ProductQueryParams queryParams)
        :base(p => 
        (!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId) &&
        (!queryParams.typeId.HasValue || p.TypeId == queryParams.typeId) &&
        (string.IsNullOrEmpty(queryParams.searchValue) || p.Name.ToLower().Contains(queryParams.searchValue.ToLower())))
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);

        switch(queryParams.sort)
        {
            case ProductSortingOptions.NameAsc:AddOrderBy(p => p.Name);
                break;
            case ProductSortingOptions.NameDesc:AddOrderByDescending(p => p.Name);
                break;
            case ProductSortingOptions.PriceAsc:AddOrderBy(p => p.Price);
                break;
            case ProductSortingOptions.PriceDesc:AddOrderByDescending(p => p.Price);
                break;
            _: break;
        }

        ApplyPagination(queryParams.pageSize, queryParams.pageIndex);
    }
    public ProductSpecifications(int id): base(p => p.Id == id)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }
}
