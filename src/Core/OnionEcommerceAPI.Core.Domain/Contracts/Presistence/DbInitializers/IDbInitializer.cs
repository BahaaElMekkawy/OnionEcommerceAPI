namespace OnionEcommerceAPI.Core.Domain.Contracts.Presistence.DbInitializers
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
