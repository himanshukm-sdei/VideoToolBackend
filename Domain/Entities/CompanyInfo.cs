using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CompanyInfo
    {
        public long CompanyId { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminCountry { get; set; }
        public string AdminProfile { get; set; }
        public string AdminState { get; set; }
        public string AdminCity { get; set; }
        public string AdminPhone { get; set; }
        public string AdminMobile { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string CompanyState { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyCountry { get; set; }
        public string Zipcode { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyFax { get; set; }
    }
}
