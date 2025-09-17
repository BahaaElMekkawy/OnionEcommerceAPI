using Microsoft.AspNetCore.Identity;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence.DbInitializers;
using OnionEcommerceAPI.Core.Domain.Entities.Identity;
using OnionEcommerceAPI.Infrastructure.Persistence.Common;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Identity
{
    internal class StoreIdentityContextInitializer : DbInitializer, IStoreIdentityContextInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public StoreIdentityContextInitializer(StoreIdentityDbContext identityDbContext, UserManager<ApplicationUser> userManager)
            : base(identityDbContext)
        {
            _userManager = userManager;
        }

        public override async Task SeedAsync()
        {
            //if (!_userManager.Users.Any())
            //{
                var user = new ApplicationUser()
                {
                    //Id = Guid.NewGuid().ToString(),
                    DisplayName = "Bahaaaa Mekkawy",
                    UserName = "bahaaaa.mekkawy",
                    Email = "bahaaaaamekkawy@gmail.com",
                    PhoneNumber = "01009305379",
                };

                await _userManager.CreateAsync(user, "Bahaa@123");
            
        }
    }
}
