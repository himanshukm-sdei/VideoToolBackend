using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SessionBookedUsers
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public bool? AllowDepartment { get; set; }
        public int SessionBookingStatus { get; set; }
        public int IsWaitingList { get; set; }
    }
}
