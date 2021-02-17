using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class CompanyList
    {
        //Company Model
        public long CompanyId { get; set; }
        public string CompanyWebsite { get; set; }
        public bool? IsActive { get; set; }
        public string CompanyName { get; set; }
        public string RateType { get; set; }
        public string ContactNo { get; set; }

    }
}
