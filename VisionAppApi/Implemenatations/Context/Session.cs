using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class Session
    {
        public Session()
        {
            SessionAttendee = new HashSet<SessionAttendee>();
            SessionBooking = new HashSet<SessionBooking>();
            SessionDocument = new HashSet<SessionDocument>();
            SessionNotes = new HashSet<SessionNotes>();
            SessionPayment = new HashSet<SessionPayment>();
            SessionRecording = new HashSet<SessionRecording>();
            SessionTag = new HashSet<SessionTag>();
        }

        public long SessionId { get; set; }
        public Guid SessionGuid { get; set; }
        public long UserId { get; set; }
        public string SessionTitle { get; set; }
        public int TopicId { get; set; }
        public int SessionType { get; set; }
        public string SessionShotDescription { get; set; }
        public string SessionDescription { get; set; }
        public DateTime? SessionDate { get; set; }
        public DateTime? SessionTime { get; set; }
        public int? SessionLength { get; set; }
        public int? NumberOfSeats { get; set; }
        public decimal? SeatPrice { get; set; }
        public bool IsWaitingList { get; set; }
        public bool IsWaitingVideoRecording { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? PublishTime { get; set; }
        public bool? IsActive { get; set; }
        public int? SessionStatus { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public long? AcceptedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public int? FirstResponder { get; set; }
        public long TimezoneId { get; set; }
        public bool? IsAccepted { get; set; }

        public virtual MasterSessionType SessionTypeNavigation { get; set; }
        public virtual MasterTimezone Timezone { get; set; }
        public virtual MasterTopic Topic { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<SessionAttendee> SessionAttendee { get; set; }
        public virtual ICollection<SessionBooking> SessionBooking { get; set; }
        public virtual ICollection<SessionDocument> SessionDocument { get; set; }
        public virtual ICollection<SessionNotes> SessionNotes { get; set; }
        public virtual ICollection<SessionPayment> SessionPayment { get; set; }
        public virtual ICollection<SessionRecording> SessionRecording { get; set; }
        public virtual ICollection<SessionTag> SessionTag { get; set; }
    }
}
