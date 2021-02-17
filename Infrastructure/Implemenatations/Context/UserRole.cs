using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class UserRole
    {
        public long UserRoleId { get; set; }
        public Guid UserRoleGuid { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual MasterRole Role { get; set; }
        public virtual Users User { get; set; }
    }
}
