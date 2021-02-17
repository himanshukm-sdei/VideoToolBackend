using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class UserMembership
    {
        public long UserMembershipId { get; set; }
        public Guid UserMembershipGuid { get; set; }
        public DateTime? MembershipStartDate { get; set; }
        public DateTime? MembershipEndDate { get; set; }
        public int? ActiveMembershipPlan { get; set; }
        public bool? IsRecurring { get; set; }
        public int? PlanId { get; set; }
        public int? InvoiceId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public long? UserId { get; set; }

        public virtual MasterPlan Plan { get; set; }
        public virtual Users User { get; set; }
    }
}
