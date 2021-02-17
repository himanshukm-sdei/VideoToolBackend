using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class NotificationRequest
    {
        public long SessionId { get; set; }
        public long UserId { get; set; }
        public int Enable { get; set; }
    }
}
