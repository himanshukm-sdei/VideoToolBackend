using System;
using System.Collections.Generic;
using System.Text;

namespace MicroservicesArchitecture.Common.Exceptions
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
