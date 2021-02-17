using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class UnblockUserRequest
    {
        public int userId { get; set; }
        public int userBlockedId { get; set; }
        public int blockedUserId { get; set; }
    }
}
