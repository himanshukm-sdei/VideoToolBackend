using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class UserBlocked
    {
        public long UserBlockedId { get; set; }
        public Guid UserBlockedGuid { get; set; }
        public long UserId { get; set; }
        public long BlockedUserId { get; set; }
        public DateTime? BlockedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Users User { get; set; }
    }
}
