namespace ECommerce.UseCases.Params;

public class ProductQueryParams
{
    public int? brandId { get; set; }
    public int? typeId { get; set; }
    public string? searchValue { get; set; }
    public ProductSortingOptions sort { get; set; }
    public int pageIndex { get; set; } = 1;
    
    private const int defaultPageSize = 5;
    private const int maxPageSize = 10;

    private int _pageSize = defaultPageSize;

    public int pageSize
    {
        get => _pageSize;
        set => _pageSize = value > maxPageSize ? maxPageSize : (value <  1 ? defaultPageSize : value);
    }
}
