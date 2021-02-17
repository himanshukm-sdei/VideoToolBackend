using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class SessionBookingRequest
    {
        public long UserId { get; set; }
        public long SessionId { get; set; }
        public DateTime BookingDate { get; set; }
        public long MemberId { get; set; }
        public int SessionType { get; set; }
    }
}
