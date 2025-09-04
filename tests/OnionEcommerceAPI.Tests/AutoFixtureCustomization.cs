using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using OnionEcommerceAPI.Core.Domain.Entities.Products;

namespace OnionEcommerceAPI.Tests
{
    internal class AutoFixtureCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Product>(composer =>
                composer.Without(p => p.Category)
                        .Without(p => p.Brand));
        }
    }
}
