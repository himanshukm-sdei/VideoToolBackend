using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class MasterInterest
    {
        public MasterInterest()
        {
            UserInterest = new HashSet<UserInterest>();
        }

        public int InterestId { get; set; }
        public string InterestName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<UserInterest> UserInterest { get; set; }
    }
}
