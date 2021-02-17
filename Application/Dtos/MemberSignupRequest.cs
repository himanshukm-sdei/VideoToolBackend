using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class MemberSignupRequest
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserPassword { get; set; }
        [Required]
        public string SecretKey { get; set; }
        public IFormFile ProfileImage { get; set; }
        public int? PlanId { get; set; }
    }
}
