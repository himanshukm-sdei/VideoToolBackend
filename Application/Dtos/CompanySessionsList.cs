using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class CompanySessionsList
    {
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public int SessionID { get; set; }
        public Guid SessionGuid { get; set; }
        public string SessionTitle { get; set; }
        public string SessionType { get; set; }
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
        public int Total { get; set; }
        public int SeatAvaiable { get; set; }
        public string Username { get; set; }
        public Guid SessionGUID { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsWaitingList { get; set; }
        public int SessionBookingStatus { get; set; }
        public int IsAccepted { get; set; }
        public string TimezoneName { get; set; }
        public string Email { get; set; }

    }
}
