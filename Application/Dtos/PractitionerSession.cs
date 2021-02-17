using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class PractitionerSession
    {
        public long SessionId { get; set; }
        public long UserId { get; set; }
        public string SessionTitle { get; set; }
        public string TopicName { get; set; }
        public string SessionType { get; set; }
        public string SessionShotDescription { get; set; }
        public string SessionDescription { get; set; }
        public DateTime? SessionDate { get; set; }
        public DateTime? SessionTime { get; set; }
        public int? SessionLength { get; set; }
        public int? NumberOfSeats { get; set; }
        public decimal? SeatPrice { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? PublishTime { get; set; }
       
    }
}
