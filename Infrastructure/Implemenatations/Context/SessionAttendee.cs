using System;
using System.Collections.Generic;

namespace Infrastructure.Implemenatations.Context
{
    public partial class SessionAttendee
    {
        public long SessionAttendeeId { get; set; }
        public Guid SessionAttendeeGuid { get; set; }
        public long SessionId { get; set; }
        public int? Attendance { get; set; }
        public int? SeatStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public long? UserId { get; set; }

        public virtual Session Session { get; set; }
        public virtual Users User { get; set; }
    }
}
