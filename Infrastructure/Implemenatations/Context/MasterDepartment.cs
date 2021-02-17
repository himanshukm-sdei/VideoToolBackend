using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class MasterDepartment
    {
        public MasterDepartment()
        {
            UserProfile = new HashSet<UserProfile>();
        }

        public long DepartmentId { get; set; }
        public Guid DepartmentGuid { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string DepartmentCode { get; set; }

        public virtual ICollection<UserProfile> UserProfile { get; set; }
    }
}
