using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{

    public class CategoryList
    {

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<SessionList> sessionLists { get; set; }
        public Sessionrecords Sessionrecords { get; set; }
    }
    public class Timezone
    {
        public string TimezoneName { get; set; }

    }
    public class SessionList
    {
        public int PractitionerId { get; set; }
        public int? Total { get; set; }
        public int TopicId { get; set; }
        public int SessionID { get; set; }
        public int? SessionBookingStatus { get; set; }
        public Guid SessionGuid { get; set; }
        public string SessionTitle { get; set; }
        public string SessionType { get; set; }
        public bool? IsBooked { get; set; }
        public decimal? SeatPrice { get; set; }
        public string PractitionerName { get; set; }
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
        public bool FirstResponder { get; set; }
        public int SeatAvaiable { get; set; }
        public string Position { get; set; }
        public List<TagsVm> Tags { get; set; }
        public string TimeZoneName { get; set; }

        public int SessionStatus { get; set; }
    }

    public class Sessionrecords
    {
        public int TotalRecords { get; set; }
        public int OpenRecords { get; set; }
        public int PrivateRecords { get; set; }
    }

    public class UsersSessionList
    {
        public DateTime? BookingDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int SessionType { get; set; }
        public int SessionBookingStatus { get; set; }
        public string SessionTitle { get; set; }
        public string SessionShotDescription { get; set; }
        public string SessionDescription { get; set; }
        public int SessionLength { get; set; }
        public DateTime SessionDate { get; set; }
        public Guid SessionGuid { get; set; }
    }
}


