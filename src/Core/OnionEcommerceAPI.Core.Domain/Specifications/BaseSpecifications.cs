using System.Linq.Expressions;
using OnionEcommerceAPI.Core.Domain.Contracts;

namespace OnionEcommerceAPI.Core.Domain.Specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();

        public BaseSpecifications() //Will be used to build a GetAll Query 
        {
            //Criteria = null;
        }
        public BaseSpecifications(TKey id) //Will be used to build a GetById Query 
        {
            Criteria = E => E.Id.Equals( id );
        }
    }
}
