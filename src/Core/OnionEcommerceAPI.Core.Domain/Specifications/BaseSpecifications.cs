using System.Linq.Expressions;
using OnionEcommerceAPI.Core.Domain.Contracts;

namespace OnionEcommerceAPI.Core.Domain.Specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; } 

        public BaseSpecifications(Expression<Func<TEntity, bool>> expression) //Will be used to build a GetAll Query 
        {
            Criteria = expression;
        }
        public BaseSpecifications(TKey id) //Will be used to build a GetById Query 
        {
            Criteria = E => E.Id.Equals(id);
        }
        private protected virtual void AddIncludes() { }
        private protected virtual void AddOrderBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy = expression;
        }
        private protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDesc = expression;
        }
        private protected virtual void AddPagination(int pageIndex, int pageSize)
        {
            IsPaginationEnabled = true;
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
        }
    }
}
