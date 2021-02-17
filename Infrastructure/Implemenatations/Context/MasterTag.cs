using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class MasterTag
    {
        public MasterTag()
        {
            SessionTag = new HashSet<SessionTag>();
        }

        public int TagId { get; set; }
        public string TagName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<SessionTag> SessionTag { get; set; }
    }
}
