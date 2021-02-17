using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class UserLogActivity
    {
        public Guid UserLogActivityGuid { get; set; }
        public long? UserId { get; set; }
        public int? LogActivityId { get; set; }
        public string Ipinformation { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual MasterUserLogActivity LogActivity { get; set; }
    }
}
