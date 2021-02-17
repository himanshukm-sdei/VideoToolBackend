using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BulkCreatedUsers
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? UserInviteGuid { get; set; }
        public int? InviteStatus { get; set; }
        public string CompanyUserNameCode { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public int CompanyAdminId { get; set; }
    }
}
