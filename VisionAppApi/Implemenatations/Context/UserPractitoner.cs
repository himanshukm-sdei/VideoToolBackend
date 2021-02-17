using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class UserPractitoner
    {
        public long UserBlockedId { get; set; }
        public Guid UserBlockedGuid { get; set; }
        public long UserId { get; set; }
        public long PractitonerUserId { get; set; }
        public DateTime? PractitoneFollowerDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Users User { get; set; }
    }
}
