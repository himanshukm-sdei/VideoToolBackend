using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class BlockUserHistory
    {
        public long HistoryId { get; set; }
        public long UserId { get; set; }
        public long? BlockedUserId { get; set; }
        public long? UnblockedUserId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
