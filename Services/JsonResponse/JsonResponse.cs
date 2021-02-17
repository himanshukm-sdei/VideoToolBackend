using System;
using System.Collections.Generic;
using System.Text;

namespace Services.JsonResponse
{
    /// <summary>
    /// response with list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonResponse<T>
    {
        public T Data { get; set; }
        public Int32 Status { get; set; }
        public string Message { get; set; }
    }
}
