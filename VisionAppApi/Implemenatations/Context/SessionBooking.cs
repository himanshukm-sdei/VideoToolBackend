using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class SessionBooking
    {
        public long SessionBookingId { get; set; }
        public Guid SessionBookingGuid { get; set; }
        public long SessionId { get; set; }
        public DateTime BookingDate { get; set; }
        public long UserId { get; set; }
        public int SessionType { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? SessionBookingStatus { get; set; }
        public bool? AllowNotification { get; set; }
        public int? IsWaitingList { get; set; }
        public long? CancelBy { get; set; }
        public DateTime? CancelDate { get; set; }

        public virtual Session Session { get; set; }
        public virtual MasterSessionType SessionTypeNavigation { get; set; }
        public virtual Users User { get; set; }
    }
}
