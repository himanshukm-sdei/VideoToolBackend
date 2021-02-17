using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Application.Dtos
{
    public class SessionDetails
    {
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public int SessionID { get; set; }
        public Guid SessionGuid { get; set; }
        public string SessionTitle { get; set; }
        public string SessionType { get; set; }
        public decimal? SeatPrice { get; set; }
        public string PractitionerName { get; set; }
        public int PractitionerId { get; set; }
        public int ProfileId { get; set; }
        public Guid ProfileGuId { get; set; }
        public int? NumberOfSeats { get; set; }
        public string ProfilePhoto { get; set; }
        public DateTime? SessionDate { get; set; }
        public DateTime? SessionTime { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? PublishTime { get; set; }
        public string TopicName { get; set; }
        public int? SessionLength { get; set; }
        public string SessionDescription { get; set; }
        public string SessionShotDescription { get; set; }
        public bool FirstResponder { get; set; }
        public bool IsWaitingList { get; set; }
        public bool IsWaitingVideoRecording { get; set; }
        public int SeatAvaiable { get; set; }
        public string Position { get; set; }
        public string BriefBio { get; set; }
        public List<TagsVm> Tags { get; set; }
        public int TimezoneId { get; set; }
        public int? IsFeature { get; set; }
        public string TimezoneName { get; set; }
        public List<SessionList>? RelatedSessions { get; set; }
        public int IsAccepted { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public long? AcceptedBy { get; set; }
    }
}
