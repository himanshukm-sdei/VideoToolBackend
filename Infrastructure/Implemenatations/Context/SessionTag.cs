using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class SessionTag
    {
        public long SessionTagId { get; set; }
        public int TagId { get; set; }
        public long SessionId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Session Session { get; set; }
        public virtual MasterTag Tag { get; set; }
    }
}
