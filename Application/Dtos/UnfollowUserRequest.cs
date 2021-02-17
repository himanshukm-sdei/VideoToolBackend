using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class UnfollowUserRequest
    {
        public int userId { get; set; }
        public int userFollowerId { get; set; }
    }
}
