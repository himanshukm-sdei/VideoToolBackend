using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SessionWaitingList
    {
        public int SessionId { get; set; }
        public string SessionTitle { get; set; }
        public string TopicName { get; set; }
        public string SessionDescription { get; set; }
        public Guid SessionGuid { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserTimezone { get; set; }
        public string SessionTimezone { get; set; }
        public DateTime? SessionDate { get; set; }
        public DateTime? SessionTime { get; set; }
    }
}
