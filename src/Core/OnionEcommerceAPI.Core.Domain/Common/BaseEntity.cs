using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnionEcommerceAPI.Core.Domain.Common
{
    public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        // The 'where TKey : IEquatable<TKey>' constraint ensures that the TKey type
        // has a type-specific, high-performance Equals method. This is crucial for
        // fast equality checks on the Id property, which happens frequently.
        // Without this constraint, using object.Equals would cause BOXING (performance hit)
        // when TKey is a value type (e.g., int, Guid) by wrapping it in an object.
        // Common key types like int, long, Guid, and string already implement this.
        public required TKey Id { get; set; } // The 'required' modifier ensures the property must be initialized during object creation.

        public required string CreatedBy { get; set; }
        public required DateTime CreatedOn { get; set; } /*= DateTime.Now; in aplication approach or sqldefault value in database but we will be make it in interceptors*/ 
        public required string LastModifiedBy { get; set; }/*will equls the created by at creation , also we can make it a null at first*/
        public required DateTime LastModifiedOn { get; set; }
    }
}
