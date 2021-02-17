using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class MasterPlan
    {
        public MasterPlan()
        {
            CompanyRatePlan = new HashSet<CompanyRatePlan>();
            UserMembership = new HashSet<UserMembership>();
        }

        public int PlanId { get; set; }
        public Guid PlanGuid { get; set; }
        public string PlanName { get; set; }
        public decimal? PlanAmount { get; set; }
        public string PlanDescription { get; set; }
        public string PlanDuration { get; set; }
        public string PlanFor { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public int? PlanMonth { get; set; }

        public virtual ICollection<CompanyRatePlan> CompanyRatePlan { get; set; }
        public virtual ICollection<UserMembership> UserMembership { get; set; }
    }
}
