using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionEcommerceAPI.Core.Domain.Common;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Data.Configurations.Base
{
    public class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(E => E.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
