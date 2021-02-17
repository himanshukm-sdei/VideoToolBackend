using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FollowedUserList
    {
        public long UserFollowerId { get; set; }
        public long FollowedUserId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime? FollowedDate { get; set; }
        public string ProfilePic { get; set; }
    }
}
