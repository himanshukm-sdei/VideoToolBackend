using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class BulkRequest
    {
        [Required]
        public IFormFile file { get; set; }
        //[Required]
        //public int CompanyId { get; set; }
        //[Required]
        //public int CompanyUserId { get; set; }
        //[Required]
        //public string CompanyUserNameCode { get; set; }
    }
}
