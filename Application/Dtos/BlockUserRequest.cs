using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class BlockUserRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BlockedOrFollowerId { get; set; }
        public bool IsBlock { get; set; }

    }
}
