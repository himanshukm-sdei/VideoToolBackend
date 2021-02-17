using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class SubscriptionInfo
    {
        public long UserMembershipId { get; set; }
        public DateTime? MembershipStartDate { get; set; }
        public DateTime? MembershipEndDate { get; set; }
        public bool? IsRecurring { get; set; }
        public bool? Active { get; set; }
        public int? PlanId { get; set; }
        public string PlanName { get; set; }
        public decimal? PlanAmount { get; set; }
        public string PlanDuration { get; set; }
    }
}
