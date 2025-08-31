using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionEcommerceAPI.Core.Domain.Common;

namespace OnionEcommerceAPI.Core.Domain.Entities.Products
{
    public class ProductBrand : BaseEntity<int>
    {
        public required string Name{ get; set; }
    }
}
