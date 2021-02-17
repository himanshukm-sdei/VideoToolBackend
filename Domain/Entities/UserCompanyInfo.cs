using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserCompanyInfo
    {
        public Guid UserCompanyGuid { get; set; }
        public long UserId { get; set; }
        public long CompanyId { get; set; }
        public string CompanyUserNameCode { get; set; }
    }
}
