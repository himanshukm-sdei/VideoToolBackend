using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class CompanyUsersRequest
    {
        public long? CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int OffSet { get; set; }
        public int Limit { get; set; }
    }

    public class CompanySessionsRequest
    {
        public long UserId { get; set; }
        public long SessionId { get; set; }
        public string SessionType { get; set; }
        public string SessionTitle { get; set; }
        public string PractitionerName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int OffSet { get; set; }
        public int Limit { get; set; }
        public Guid SessionGuid { get; set; }
        public int IsAccepted { get;set; }

    }

    public class SessionsTypes
    {
        public long TypeId { get; set; }
        public string TypeName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int OffSet { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; } 
        public int SessionsCount { get; set; }


    }
}
