using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionEcommerceAPI.Core.Application.Common.Exception
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key)
            : base($"{name} With [{key}] Is Not Found.")
        {
        }
    }
}
