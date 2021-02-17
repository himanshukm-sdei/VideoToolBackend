using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class MasterSessionType
    {
        public MasterSessionType()
        {
            Session = new HashSet<Session>();
            SessionBooking = new HashSet<SessionBooking>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Session> Session { get; set; }
        public virtual ICollection<SessionBooking> SessionBooking { get; set; }
    }
}
