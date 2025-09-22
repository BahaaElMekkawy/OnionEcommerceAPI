﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionEcommerceAPI.Core.Application.Common.Exception
{
    public class ValidationException :BadRequestException
    {
        public IEnumerable<string> Errors { get; set; }
        public ValidationException(string message = "Bad Request")
            :base(message)
        {
            
        }
    }
}
