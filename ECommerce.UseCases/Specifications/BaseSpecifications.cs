using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using System.Linq.Expressions;

namespace ECommerce.UseCases.Specifications;

public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
{

    #region Where
    public Expression<Func<TEntity, bool>> Criteria { get; private set; }

    protected BaseSpecifications(Expression<Func<TEntity, bool>> Crit = null!)
    {
        Criteria = Crit;
    }
    #endregion

    #region Include
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; private set; } = [];

    public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        IncludeExpressions.Add(includeExpression);
    }
    #endregion

    #region OrderBy

    public Expression<Func<TEntity, object>>? OrderBy {  get; private set; }

    public void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        => OrderBy = orderByExpression;
    
    public Expression<Func<TEntity, object>>? OrderByDescending {  get; private set; }


    public void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        => OrderByDescending = orderByDescExpression;


    #endregion

    #region Pagination
    public int Take {  get; private set; }

    public int Skip { get; private set; }

    public bool IsPaginated { get; private set; }

    public void ApplyPagination(int pageSize, int pageIndex)
    {
        Take = pageSize;
        Skip = (pageIndex - 1) * pageSize;
        IsPaginated = true;
    }
    #endregion
}
