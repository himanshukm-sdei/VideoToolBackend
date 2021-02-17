using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public partial class SessionBookingData
    {
        public long SessionBookingId { get; set; }
        public Guid SessionBookingGuid { get; set; }
        public Guid SessionGuid { get; set; }
        public long SessionId { get; set; }
        public DateTime BookingDate { get; set; }
        public string SessionTopic { get; set; }
        public DateTime? SessionDate { get; set; }
        public string SessionShotDescription { get; set; }
        public DateTime? SessionTime { get; set; }
        public long UserId { get; set; }
        public string SessionTitle { get; set; }
        public string SessionDescription { get; set; }
        public string SessionType { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? SessionBookingStatus { get; set; }
        public bool IsWaitingList { get; set; }
        public bool IsBooked { get; set; }

        public string Email { get; set; }
        public string PractitionerName { get; set; }
        public string UserTimezone { get; set; }
        public string SessionTimezone { get; set; }


    }
}
