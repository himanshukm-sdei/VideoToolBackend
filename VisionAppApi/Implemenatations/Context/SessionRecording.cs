using System;
using System.Collections.Generic;

namespace VisionAppApi.Implemenatations.Context
{
    public partial class SessionRecording
    {
        public long SessionRecordingId { get; set; }
        public Guid SessionRecordingGuid { get; set; }
        public long SessionId { get; set; }
        public long PractitionerId { get; set; }
        public string RecordingName { get; set; }
        public DateTime? RecordingDate { get; set; }
        public DateTime? RecordingTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Users Practitioner { get; set; }
        public virtual Session Session { get; set; }
    }
}
