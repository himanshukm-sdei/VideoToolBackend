using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class ProfileRequest
    {
        public IFormFile file { get; set; }
        public int userId { get; set; }
    }
}
