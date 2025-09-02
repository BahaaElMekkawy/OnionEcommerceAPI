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
    }
}
