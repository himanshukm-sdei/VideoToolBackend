using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class DepartmentDetails
    {
        public long? DepartmentId { get; set; }
        public bool DepartmentStatus { get; set; }
        public long ProfileId { get; set; }
        public long UserId { get; set; }
        public bool? AllowDepartment { get; set; }
        public string DepartmentName { get; set; }
    }
}
