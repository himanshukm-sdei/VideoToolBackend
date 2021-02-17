using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CompanyMembers
    {
        public long UserId { get; set; }
        public Guid ProfileGuid { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public bool? Block { get; set; }
        public int Total { get; set; }
    }
}
