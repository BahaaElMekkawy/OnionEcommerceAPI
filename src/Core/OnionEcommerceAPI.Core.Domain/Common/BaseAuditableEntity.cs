using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionEcommerceAPI.Core.Domain.Common
{
    public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public required string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow; /*in aplication approach or sqldefault value in database but we will be make it in interceptors*/
        public required string LastModifiedBy { get; set; }/*will equls the created by at creation , also we can make it a null at first*/
        public DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;
    }
}
