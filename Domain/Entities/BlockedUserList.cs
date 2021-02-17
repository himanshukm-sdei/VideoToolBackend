using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BlockedUserList
    {
        public long UserBlockedId { get; set; }
        public long UserId { get; set; }
        public long BlockedUserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? BlockedDate { get; set; }

    }
}
