using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class RatePlanInfo
    {
        public string PlanName { get; set; }
        public decimal? PlanAmount { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? PlanEndDate { get; set; }
        public long PlanId { get; set; }
        public long CompanyRateTableId { get; set; }
        public bool? IsRecurring { get; set; }
        public bool? Active { get; set; }
    }
}
