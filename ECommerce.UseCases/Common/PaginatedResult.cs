namespace ECommerce.UseCases.Common;

public class PaginatedResult<TEntity>
{
    public PaginatedResult(IReadOnlyList<TEntity> data, int pageIndex, int pageSize, int count)
    {
        Data = data;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
    }

    public IReadOnlyList<TEntity> Data { get; set; } = [];

    public int PageIndex { get; }
    public int PageSize { get; }
    public int Count { get;  }

}
