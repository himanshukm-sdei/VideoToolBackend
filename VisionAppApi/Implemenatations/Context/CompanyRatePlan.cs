using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class CompanyRatePlan
    {
        public long CompanyRateTableId { get; set; }
        public Guid CompanyRateTableGuid { get; set; }
        public long CompanyId { get; set; }
        public int? PlanId { get; set; }
        public int? ActivePlan { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? PlanEndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecurring { get; set; }

        public virtual Company Company { get; set; }
        public virtual MasterPlan Plan { get; set; }
    }
}
